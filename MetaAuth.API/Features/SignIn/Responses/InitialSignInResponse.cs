using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignIn.Responses;

public class InitialSignInResponse : BaseResponse
{
    public required string InitialSignInGuid { get; set; }
}