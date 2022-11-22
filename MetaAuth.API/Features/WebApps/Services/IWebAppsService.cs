using MetaAuth.API.Features.WebApps.Requests;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.API.Features.WebApps.Services;

public interface IWebAppsService
{
    Task<RegisteredWebAppsModel?> GetRegisteredApp(GetRegisteredAppRequest request);
}