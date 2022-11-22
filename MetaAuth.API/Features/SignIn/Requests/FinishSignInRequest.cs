using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.SignIn.Requests;

public class FinishSignInRequest : IHttpRequest
{
    public required string Id { get; set; }
    public required string AppName { get; set; }
    public required string ReturnUrl { get; set; } 
    public string? AccessToken { get; set; }
    public required DateTime RequestCreation { get; set; }
    public required bool Finished { get; set; }
    public required bool Success { get; set; }
}