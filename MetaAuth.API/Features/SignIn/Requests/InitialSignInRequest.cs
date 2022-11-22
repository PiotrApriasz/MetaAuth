using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.SignIn.Requests;

public class InitialSignInRequest : IHttpRequest
{
    public string AppName { get; set; } = null!;
    public string ReturnUrl { get; set; } = null!;
}