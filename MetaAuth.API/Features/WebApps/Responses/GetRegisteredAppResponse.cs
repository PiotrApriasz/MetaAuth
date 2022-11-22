using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;
using Microsoft.AspNetCore.Authentication;

namespace MetaAuth.API.Features.WebApps.Responses;

public class GetRegisteredAppResponse : BaseResponse
{
    public required  RegisteredWebAppsModel RegisteredWebAppsModel { get; set; }
}