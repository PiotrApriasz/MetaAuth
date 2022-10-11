using MediatR;
using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.API.Features.SignUp.Responses;
using MetaAuth.API.Features.SignUp.Services;

namespace MetaAuth.API.Features.SignUp.Handlers;

public class InitialSignUpHandler : IRequestHandler<InitialSignUpRequest, IResult>
{
    private readonly ISignUpService _signUpService;

    public InitialSignUpHandler(ISignUpService signUpService)
    {
        _signUpService = signUpService;
    }

    public async Task<IResult> Handle(InitialSignUpRequest request, CancellationToken cancellationToken)
    {
        var requestId = await _signUpService.RegisterMetaAuthSignUp(request);
        
        return Results.Ok(new InitialSignUpResponse()
        {
            Error = false,
            Message = "Sign up operation has been successfully registered",
            RequestGuid = requestId
        });
    }
}