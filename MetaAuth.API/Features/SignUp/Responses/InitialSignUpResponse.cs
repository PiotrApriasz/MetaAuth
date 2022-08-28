using MetaAuth.API.Core.Response;

namespace MetaAuth.API.Features.SignUp.Responses;

public class InitialSignUpResponse : BaseResponse
{
    public string RequestGuid { get; set; }
}