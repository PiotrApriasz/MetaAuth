using Microsoft.Azure.Cosmos;

namespace MetaAuth.API.Features.SignIn.Services;

public class SignInService : ISignInService
{
    private readonly CosmosClient _cosmosClient;

    public SignInService(IConfiguration configuration)
    {
        _cosmosClient = new CosmosClient(configuration["MetaAuthDb:Uri"], 
            configuration["MetaAuthDb:Key"]);
    }
    
    
}