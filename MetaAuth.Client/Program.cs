using MetaAuth.Client;
using MetaAuth.Logic.Services;
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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSingleton<IMetamaskInterop, MetamaskBlazorInterop>();
builder.Services.AddSingleton<MetamaskInterceptor>();
builder.Services.AddSingleton<MetamaskHostProvider>();

builder.Services.AddSingleton(services =>
{
    var metamaskHostProvider = services.GetService<MetamaskHostProvider>();
    var selectedHostProvider = new SelectedEthereumHostProviderService();
    selectedHostProvider.SetSelectedEthereumHostProvider(metamaskHostProvider);
    return selectedHostProvider;
});

builder.Services.AddScoped<ITokenService>(services =>
{
    var selectedEthereumHost = services.GetService<SelectedEthereumHostProviderService>();
    if (selectedEthereumHost == null)
        throw new NullReferenceException("Service is unable to connect with web3 provider");
    var web3 = selectedEthereumHost.SelectedHost.GetWeb3Async().Result;
    return new TokenService(web3, builder.Configuration["SmartContractAddress"]!);

});

builder.Services.AddSingleton<AuthenticationStateProvider, EthereumAuthenticationStateProvider>();

await builder.Build().RunAsync();