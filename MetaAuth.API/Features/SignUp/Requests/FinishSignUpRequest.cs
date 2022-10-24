using MetaAuth.API.Core.Endpoints;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.API.Features.SignUp.Requests;

public class FinishSignUpRequest : IHttpRequest
{
    public string Id { get; set; } = null!;
    public string AppName { get; set; } = null!;
    public List<string> RequiredUserData { get; set; } = null!;
    public string UserIdentificator { get; set; } = null!;
    public DateTime RequestCreation { get; set; }
    public string? UserPublicWalletAddress { get; set; } = null!;
    public bool Finished { get; set; }
    public bool Success { get; set; }
    public string ReturnUrl { get; set; }
}