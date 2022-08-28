﻿using System.Net;
using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.SharedEntities;
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
        var signUpEnt = new SignUpModel()
        {
            Id = Guid.NewGuid().ToString(),
            AppName = request.AppName,
            Finished = false,
            RequestCreation = DateTime.Now,
            RequiredUserData = request.RequiredUserData,
            UserIdentificator = request.UserIdentificator
        };

        await _signUpContainer.CreateItemAsync(signUpEnt, new PartitionKey(signUpEnt.AppName));

        return signUpEnt.Id;
    }
}