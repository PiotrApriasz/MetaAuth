﻿using System.Net;
using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.SharedEntities.AzureCosmosDb;
using Microsoft.Azure.Cosmos;

namespace MetaAuth.API.Features.SignUp.Services;

public class SignUpService : ISignUpService
{
    private readonly Container _signUpContainer;

    public SignUpService(IConfiguration configuration)
    {
        var cosmosClient = new CosmosClient(configuration["MetaAuthDb:Uri"], 
            configuration["MetaAuthDb:Key"]);
        _signUpContainer = cosmosClient.GetContainer("MetaAuth", "SignUpRequests");
    }

    public async Task<string> RegisterMetaAuthSignUp(InitialSignUpRequest request)
    {
        var signUpEnt = new SignUpModel
        {
            Id = Guid.NewGuid()
                .ToString(),
            AppName = request.AppName,
            Finished = false,
            RequestCreation = DateTime.Now,
            UserPublicWalletAddress = null,
            RequiredUserData = request.RequiredUserData,
            UserIdentificator = request.UserIdentificator,
            ReturnUrl = request.ReturnUrl,
            Success = false
        };

        await _signUpContainer.CreateItemAsync(signUpEnt, new PartitionKey(signUpEnt.AppName));

        return signUpEnt.Id;
    }

    public async Task<SignUpModel?> GetSignUpData(GetSignUpDataRequest request)
    {
        var query = new QueryDefinition(
                query: "SELECT * FROM c WHERE c.id = @requestId")
            .WithParameter("@requestId", request.RequestId);

        using var feed = _signUpContainer.GetItemQueryIterator<SignUpModel>(
            queryDefinition: query);
        var response = await feed.ReadNextAsync();
        return response.FirstOrDefault();
    }

    public async Task FinishSignUp(FinishSignUpRequest request)
    {
        var signUpEnt = new SignUpModel
        {
            Id = request.Id,
            AppName = request.AppName,
            RequiredUserData = request.RequiredUserData,
            UserIdentificator = request.UserIdentificator,
            RequestCreation = request.RequestCreation,
            UserPublicWalletAddress = request.UserPublicWalletAddress,
            Finished = request.Finished,
            Success = request.Success,
            ReturnUrl = request.ReturnUrl
        };
        
        await _signUpContainer.UpsertItemAsync(signUpEnt);
    }
    
}