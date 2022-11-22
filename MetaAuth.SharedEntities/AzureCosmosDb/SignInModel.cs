namespace MetaAuth.SharedEntities.AzureCosmosDb;

public class SignInModel : AzCosmosContainerModel
{
    public string AppName { get; set; } = null!;
    public string ReturnUrl { get; set; } = null!;
    public string? AccessToken { get; set; }
    public DateTime RequestCreation { get; set; }
    public bool Finished { get; set; }
    public bool Success { get; set; }
}