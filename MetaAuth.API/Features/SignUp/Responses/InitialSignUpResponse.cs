

using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignUp.Responses;

public class InitialSignUpResponse : BaseResponse
{
    public string RequestGuid { get; set; } = null!;
}