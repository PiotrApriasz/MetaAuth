using MetaAuth.BlazorTest;
using MetaAuth.Metamask.Blazor;
using MetaAuth.Metamask.Ethereum;
using MetaAuth.Metamask.Metamask;
using MetaAuth.MTA;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSingleton<IMetamaskInterop, MetamaskBlazorInterop>();
builder.Services.AddSingleton<MetamaskInterceptor>();
builder.Services.AddSingleton<MetamaskHostProvider>();
builder.Services.AddSingleton<IEthereumHostProvider>(serviceProvider => serviceProvider
    .GetService<MetamaskHostProvider>() ?? throw new InvalidOperationException());
builder.Services.AddSingleton<NethereumAuthenticator>();
builder.Services.AddSingleton<IMetamaskService, MetamaskService>();

await builder.Build().RunAsync();
