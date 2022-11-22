using MetaAuth.API.Features.WebApps.Requests;
using MetaAuth.SharedEntities.AzureCosmosDb;
using Microsoft.Azure.Cosmos;

namespace MetaAuth.API.Features.WebApps.Services;

public class WebAppService : IWebAppsService
{
    private readonly Container _webAppsContainer;

    public WebAppService(IConfiguration configuration)
    {
        var cosmosClient = new CosmosClient(configuration["MetaAuthDb:Uri"], 
            configuration["MetaAuthDb:Key"]);
        
        _webAppsContainer = cosmosClient.GetContainer("MetaAuth", "RegisteredWebApps");
    }

    public async Task<RegisteredWebAppsModel?> GetRegisteredApp(GetRegisteredAppRequest request)
    {
        var query = new QueryDefinition(
                query: "SELECT * FROM c WHERE c.id = @appAddress")
            .WithParameter("@appAddress", request.WebAppAddress);

        using var feed = _webAppsContainer.GetItemQueryIterator<RegisteredWebAppsModel>(
            queryDefinition: query);
        var response = await feed.ReadNextAsync();
        return response.FirstOrDefault();
    }
}