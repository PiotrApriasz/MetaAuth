using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.SignIn.Responses;

public class GetJwtTokenResponse : BaseResponse
{
    public string JwtToken { get; set; }
}