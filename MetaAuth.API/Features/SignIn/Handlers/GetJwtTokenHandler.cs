using MediatR;
using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.API.Features.SignIn.Responses;
using MetaAuth.API.Features.SignIn.Services;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignIn.Handlers;

public class GetJwtTokenHandler : IRequestHandler<GetJwtTokenRequest, IResult>
{
    private readonly ISignInService _signInService;

    public GetJwtTokenHandler(ISignInService signInService)
    {
        _signInService = signInService;
    }

    public async Task<IResult> Handle(GetJwtTokenRequest request, CancellationToken cancellationToken)
    {
        var jwtToken = await _signInService.GetAccessToken(request);
        
        if (jwtToken is not null)
        {
            return Results.Ok(new GetJwtTokenResponse
            {
                Error = false,
                Message = "Jwt token has been successfully gathered from database",
                JwtToken = jwtToken
            });
        }

        return Results.NotFound(new BaseResponse
        {
            Error = true,
            Message = "Jwt token cannot be found for provided request id"
        });
    }
}