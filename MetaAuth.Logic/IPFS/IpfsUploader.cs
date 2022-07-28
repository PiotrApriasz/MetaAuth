using System.Net.Http.Headers;
using System.Text;
using MetaAuth.Logic.Entities;
using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;
using Newtonsoft.Json;

namespace MetaAuth.Logic.IPFS;

public class IpfsUploader
{
    private readonly AuthenticationHeaderValue _authHeaderValue;
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public IpfsUploader(string url, string userName, string password, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;

        var byteArray = Encoding.UTF8.GetBytes(userName + ":" + password);
        _authHeaderValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }
    
    public async Task<IpfsFileInfo> AddObjectAsJson(MetaAuthUserData objectToSerialise, string fileName, bool pin = true)
    {
        await using var ms = new MemoryStream();
        var serializer = new JsonSerializer();
        var jsonTextWriter = new JsonTextWriter(new StreamWriter(ms));
        serializer.Serialize(jsonTextWriter, objectToSerialise);
        await jsonTextWriter.FlushAsync();
        ms.Position = 0;
        var node = await AddAsync(ms.ToArray(), fileName, true);
        return node;
    }

    private async Task<IpfsFileInfo> AddAsync(byte[] fileBytes, string fileName, bool pin = true)
    {
        var content = new MultipartFormDataContent();
        var streamContent = new ByteArrayContent(fileBytes);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(streamContent, "file", fileName);


        _httpClient.DefaultRequestHeaders.Authorization = _authHeaderValue;
        var query = pin ? "?pin=true&cid-version=1" : "?cid-version=1";
        var fullUrl = "https://ipfs.infura.io:5001/api/v0/add" + query;
        var httpResponseMessage = await _httpClient.PostAsync(fullUrl, content);
        httpResponseMessage.EnsureSuccessStatusCode();
        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();

        using var streamReader = new StreamReader(stream);
        using var reader = new JsonTextReader(streamReader);
        var serializer = JsonSerializer.Create();
        var message = serializer.Deserialize<IpfsFileInfo>(reader);
        

        return message;
    }
}