using FluentValidation;
using MetaAuth.API.Core.Endpoints;
using MetaAuth.API.Core.Features;
using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.API.Features.SignUp.Services;
using MetaAuth.API.Features.SignUp.Validators;

namespace MetaAuth.API.Features.SignUp;

public class SignUpFeature : IFeature
{
    public IServiceCollection RegisterFeature(IServiceCollection builder)
    {
        builder.AddScoped<ISignUpService, SignUpService>();
        builder.AddScoped<IValidator<InitialSignUpRequest>, InitialSignUpValidator>();
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost<InitialSignUpRequest>("signup");
        endpoints.MapGet<GetSignUpDataRequest>("signup/{RequestId}", false);
        return endpoints;
    }
}