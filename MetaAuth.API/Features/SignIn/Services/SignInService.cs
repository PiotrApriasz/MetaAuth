using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.SharedEntities.AzureCosmosDb;
using Microsoft.Azure.Cosmos;

namespace MetaAuth.API.Features.SignIn.Services;

public class SignInService : ISignInService
{
    private readonly Container _signInContainer;

    public SignInService(IConfiguration configuration)
    {
        var cosmosClient = new CosmosClient(configuration["MetaAuthDb:Uri"], 
            configuration["MetaAuthDb:Key"]);
        _signInContainer = cosmosClient.GetContainer("MetaAuth", "SignInRequests");
    }

    public async Task<string> RegisterMetaAuthSignIn(InitialSignInRequest request)
    {
        var signInEnt = new SignInModel
        {
            Id = Guid.NewGuid()
                .ToString(),
            AppName = request.AppName,
            ReturnUrl = request.ReturnUrl,
            RequestCreation = DateTime.Now,
            Finished = false,
            Success = false
        };
        
        await _signInContainer.CreateItemAsync(signInEnt, new PartitionKey(signInEnt.AppName));
        
        return signInEnt.Id;
    }

    public async Task<SignInModel?> GetSignInData(GetSignInDataRequest request)
    {
        var query = new QueryDefinition(
                query: "SELECT * FROM c WHERE c.id = @requestId")
            .WithParameter("@requestId", request.RequestId);

        using var feed = _signInContainer.GetItemQueryIterator<SignInModel>(
            queryDefinition: query);
        var response = await feed.ReadNextAsync();
        return response.FirstOrDefault();
    }

    public async Task FinishSignIn(FinishSignInRequest request)
    {
        var signInEnt = new SignInModel
        {
            Id = request.Id,
            AppName = request.AppName,
            ReturnUrl = request.ReturnUrl,
            AccessToken = request.AccessToken,
            RequestCreation = request.RequestCreation,
            Finished = request.Finished,
            Success = request.Success
        };

        await _signInContainer.UpsertItemAsync(signInEnt);
    }
    
    
}