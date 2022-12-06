using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.SignIn.Requests;

public class GetJwtTokenRequest : IHttpRequest
{
    public required string RequestId { get; set; }
}