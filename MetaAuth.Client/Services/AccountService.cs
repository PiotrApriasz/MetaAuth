using System.Net.Http.Json;
using MetaAuth.Client.Entities;
using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Client.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _client;
    private readonly string _baseAddress;

    public AccountService(HttpClient client, IConfiguration config)
    {
        _client = client;
        _baseAddress = config["MetaAuthApi:Url"];
    }

    public async Task<SignUpData?> GetSignUpData(string requestId)
    {
        var uri = $"{_baseAddress}signup/{requestId}";
        var result = await _client.GetFromJsonAsync<SignUpData>(uri);
        return result;
    }

    public async Task<BaseResponse> FinishSignUp(SignUpModel signUpModel)
    {
        var uri = $"{_baseAddress}signup/finish";
        var result = await _client.PostAsJsonAsync(uri, signUpModel);

        var response = await result.Content.ReadFromJsonAsync<BaseResponse>();

        return response!;
    }
}