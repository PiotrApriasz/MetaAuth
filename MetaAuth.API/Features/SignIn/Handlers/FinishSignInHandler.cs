using MediatR;
using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.API.Features.SignIn.Services;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignIn.Handlers;

public class FinishSignInHandler : IRequestHandler<FinishSignInRequest, IResult>
{
    private readonly ISignInService _signInService;


    public FinishSignInHandler(ISignInService signInService)
    {
        _signInService = signInService;
    }

    public async Task<IResult> Handle(FinishSignInRequest request, CancellationToken cancellationToken)
    {
        await _signInService.FinishSignIn(request);
        
        return Results.Ok(new BaseResponse
        {
            Error = false,
            Message = "SignIn request has been updated"
        });
    }
}