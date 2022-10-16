using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.SignUp.Requests;

public class InitialSignUpRequest : IHttpRequest
{
    public string AppName { get; set; } = null!;
    public List<string> RequiredUserData { get; set; } = new();
    public string UserIdentificator { get; set; } = null!;
}