using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.SignIn.Requests;

public class GetSignInDataRequest : IHttpRequest        
{
    public required string RequestId { get; set; }
}