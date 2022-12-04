using MetaAuth.API.Core.Endpoints;
using MetaAuth.API.Core.Features;
using MetaAuth.API.Features.SignIn.Requests;
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
        endpoints.MapPost<InitialSignInRequest>("signIn", false);
        endpoints.MapGet<GetSignInDataRequest>("signIn/{RequestId}", false);
        endpoints.MapPost<FinishSignInRequest>("signIn/finish", false);
        return endpoints;
    }
}