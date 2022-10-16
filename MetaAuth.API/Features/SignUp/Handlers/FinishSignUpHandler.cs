using MediatR;
using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.API.Features.SignUp.Services;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignUp.Handlers;

public class FinishSignUpHandler : IRequestHandler<FinishSignUpRequest, IResult>
{
    private readonly ISignUpService _signUpService;

    public FinishSignUpHandler(ISignUpService signUpService)
    {
        _signUpService = signUpService;
    }

    public async Task<IResult> Handle(FinishSignUpRequest request, CancellationToken cancellationToken)
    {
        await _signUpService.FinishSignUp(request);

        return Results.Ok(new BaseResponse
        {
            Error = false,
            Message = "SignUp request has been updated"
        });
    }
}