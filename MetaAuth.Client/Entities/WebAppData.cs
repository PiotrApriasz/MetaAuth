using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Client.Entities;

public class WebAppData : BaseResponse
{
    public RegisteredWebAppsModel RegisteredWebAppsModel { get; set; } = null!;
}