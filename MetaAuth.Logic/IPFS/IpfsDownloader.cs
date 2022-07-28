using MetaAuth.Logic.Entities;
using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;
using Newtonsoft.Json;

namespace MetaAuth.Logic.IPFS;

public class IpfsDownloader
{
    private string _cid;
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public IpfsDownloader(string url, string cid, HttpClient httpClient)
    {
        _url = url;
        _cid = cid;
        _httpClient = httpClient;
    }

    public async Task<MetaAuthUserData> GetAsync()
    {
        var s = await _httpClient.GetStreamAsync("https://meta-auth.infura-ipfs.io/ipfs/" + _cid);
        await using (s)
        {
            using var sr = new StreamReader(s);
            using JsonReader reader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();
            var metaAuthMetadata = serializer.Deserialize<MetaAuthUserData>(reader);

            return metaAuthMetadata;
        }
        
    }
    
}