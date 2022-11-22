using MetaAuth.API.Core.Endpoints;
using MetaAuth.API.Core.Features;
using MetaAuth.API.Features.WebApps.Requests;
using MetaAuth.API.Features.WebApps.Services;

namespace MetaAuth.API.Features.WebApps;

public class WebAppsFeature : IFeature
{
    public IServiceCollection RegisterFeature(IServiceCollection builder)
    {
        builder.AddScoped<IWebAppsService, WebAppService>();
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet<GetRegisteredAppRequest>("getApp/{WebAppAddress}", false);
        return endpoints;
    }
}