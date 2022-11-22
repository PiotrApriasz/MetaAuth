namespace MetaAuth.SharedEntities.AzureCosmosDb;

public class RegisteredWebAppsModel : AzCosmosContainerModel    
{
    public required string AppName { get; set; }
    public required string AppSecret { get; set; }
    public required int TokenExpire { get; set; }
    public required TokenExpireTimeUnits TokenExpireTimeUnit { get; set; }
}