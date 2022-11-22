using System.Drawing;
using MediatR;
using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.API.Features.SignIn.Responses;
using MetaAuth.API.Features.SignIn.Services;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignIn.Handlers;

public class GetSignInDataHandler : IRequestHandler<GetSignInDataRequest, IResult>
{
    private readonly ISignInService _signInService;

    public GetSignInDataHandler(ISignInService signInService)
    {
        _signInService = signInService;
    }

    public async Task<IResult> Handle(GetSignInDataRequest request, CancellationToken cancellationToken)
    {
        var signInData = await _signInService.GetSignInData(request);

        if (signInData is not null)
        {
            return Results.Ok(new GetSignInDataResponse
            {
                Error = false,
                Message = "Sign In data has been successfully gathered from database",
                SignInModel = signInData
            });
        }

        return Results.NotFound(new BaseResponse
        {
            Error = true,
            Message = "Sign In data cannot be found for provided request id"
        });
    }
}