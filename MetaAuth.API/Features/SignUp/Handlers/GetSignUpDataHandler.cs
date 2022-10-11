using MediatR;
using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.API.Features.SignUp.Responses;
using MetaAuth.API.Features.SignUp.Services;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignUp.Handlers;

public class GetSignUpDataHandler : IRequestHandler<GetSignUpDataRequest, IResult>
{
    private readonly ISignUpService _signUpService;


    public GetSignUpDataHandler(ISignUpService signUpService)
    {
        _signUpService = signUpService;
    }

    public async Task<IResult> Handle(GetSignUpDataRequest request, CancellationToken cancellationToken)
    {
        var data = await _signUpService.GetSignUpData(request);

        if (data is not null)
        {
            return Results.Ok(new GetSignUpDataResponse
            {
                Error = false,
                Message = "Sign Up data has been successfully gathered from database",
                SignUpModel = data
            });
        }

        return Results.NotFound(new BaseResponse()
        {
            Error = true,
            Message = "Sign up data cannot be found for provided request id"
        });
    }
}