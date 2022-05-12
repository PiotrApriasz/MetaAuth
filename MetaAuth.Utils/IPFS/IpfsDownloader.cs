using MetaAuth.Utils.IPFS.Entities;
using Newtonsoft.Json;

namespace MetaAuth.Utils.IPFS;

public class IpfsDownloader
{
    private string _cid;
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public IpfsDownloader(string url, string cid, HttpClient httpClient)
    {
        _url = url + "/ipfs/";
        _cid = cid;
        _httpClient = httpClient;
    }

    public async Task<MetaAuthMetadata> GetAsync()
    {
        await using var s = _httpClient.GetStreamAsync(_url + _cid).Result;
        using var sr = new StreamReader(s);
        using JsonReader reader = new JsonTextReader(sr);
        var serializer = new JsonSerializer();
        var metaAuthMetadata = serializer.Deserialize<MetaAuthMetadata>(reader);

        return metaAuthMetadata;
    }
    
}