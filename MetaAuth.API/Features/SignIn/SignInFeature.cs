using MetaAuth.API.Core.Features;
using MetaAuth.API.Features.SignIn.Services;

namespace MetaAuth.API.Features.SignIn;

public class SignInFeature : IFeature
{
    public IServiceCollection RegisterFeature(IServiceCollection builder)
    {
        builder.AddScoped<ISignInService, SignInService>();
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}