using System.Net.Http.Json;
using MetaAuth.Client.Entities;

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
}