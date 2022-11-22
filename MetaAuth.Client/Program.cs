using MetaAuth.Client;
using MetaAuth.Client.Services;
using MetaAuth.Core.Entities.IPFS;
using MetaAuth.Core.IPFS;
using MetaAuth.Core.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Nethereum.Blazor;
using Nethereum.Metamask;
using Nethereum.Metamask.Blazor;
using Nethereum.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient());
builder.Services.AddMudServices();

builder.Services.AddSingleton<IMetamaskInterop, MetamaskBlazorInterop>();
builder.Services.AddSingleton<MetamaskInterceptor>();
builder.Services.AddSingleton<MetamaskHostProvider>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddSingleton(services =>
{
    var metamaskHostProvider = services.GetService<MetamaskHostProvider>();
    var selectedHostProvider = new SelectedEthereumHostProviderService();
    selectedHostProvider.SetSelectedEthereumHostProvider(metamaskHostProvider);
    return selectedHostProvider;
});

builder.Services.AddScoped<IIpfsService>(services =>
{
    var ipfsConfig = new IpfsConfig
    {
        IpfsGateway = builder.Configuration["IPFSGateway"]!,
        IpfsKey = builder.Configuration["IPFSKey"]!,
        IpfsProjectId = builder.Configuration["IPFSId"]!,
        IpfsServiceBaseUrl = builder.Configuration["IPFSServiceBaseURL"]!
    };

    return new IpfsService(ipfsConfig, services.GetService<HttpClient>()
                                       ?? throw new InvalidOperationException("Unable to provide HttpClient"));
});

builder.Services.AddScoped<ITokenService>(services =>
{
    var selectedEthereumHost = services.GetService<SelectedEthereumHostProviderService>();
    if (selectedEthereumHost == null)
        throw new NullReferenceException("Service is unable to connect with web3 provider");
    var web3 = selectedEthereumHost.SelectedHost.GetWeb3Async().Result;
    return new TokenService(web3, builder.Configuration["SmartContractAddress"]!, 
        services.GetService<IIpfsService>() 
        ?? throw new InvalidOperationException
            ($"Unable to provide service {nameof(IIpfsService)}"),
        services.GetService<IJwtService>()
        ?? throw new InvalidOperationException
            ($"Unable to provide service {nameof(IJwtService)}"));
});

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSingleton<AuthenticationStateProvider, EthereumAuthenticationStateProvider>();

await builder.Build().RunAsync();