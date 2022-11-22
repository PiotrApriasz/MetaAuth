using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Core.Services;

public interface IJwtService
{
    string GenerateToken(Dictionary<string, string> userData, RegisteredWebAppsModel registeredWebAppsModel);
}