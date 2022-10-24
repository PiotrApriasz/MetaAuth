using Newtonsoft.Json;

namespace MetaAuth.SharedEntities.AzureCosmosDb;

public class SignUpModel : AzCosmosContainerModel
{
    public string AppName { get; set; }
    public List<string> RequiredUserData { get; set; }
    public string UserIdentificator { get; set; }
    public DateTime RequestCreation { get; set; }
    public string? UserPublicWalletAddress { get; set; }
    public bool Finished { get; set; }
    public string ReturnUrl { get; set; }
    public bool Success { get; set; }
}