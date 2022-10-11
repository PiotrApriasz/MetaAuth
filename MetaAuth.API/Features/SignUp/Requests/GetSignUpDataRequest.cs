using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.SignUp.Requests;

public class GetSignUpDataRequest : IHttpRequest
{
    public string RequestId { get; set; }
}