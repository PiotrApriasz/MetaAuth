using MediatR;
using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.API.Features.SignIn.Responses;
using MetaAuth.API.Features.SignIn.Services;

namespace MetaAuth.API.Features.SignIn.Handlers;

public class InitialSignInHandler : IRequestHandler<InitialSignInRequest, IResult>
{
    private readonly ISignInService _signInService;

    public InitialSignInHandler(ISignInService signInService)
    {
        _signInService = signInService;
    }

    public async Task<IResult> Handle(InitialSignInRequest request, CancellationToken cancellationToken)
    {
        var requestId = await _signInService.RegisterMetaAuthSignIn(request);

        return Results.Ok(new InitialSignInResponse
        {
            Error = false,
            Message = "Sign In operation has been successfully registered",
            InitialSignInGuid = requestId
        });
    }
}