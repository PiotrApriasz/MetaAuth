using Ipfs.Http;
using MetaAuth.Utils.IPFS.Entities;

namespace MetaAuth.Utils.IPFS;

public class IpfsService
{
    private readonly string _userName;
    private readonly string _password;
    private readonly string _ipfsUrl;
    private readonly string _ipfsGateway;
    private readonly HttpClient _httpClient;

    public IpfsService(string userName, string password, string ipfsUrl, string ipfsGateway, HttpClient httpClient)
    {
        _ipfsUrl = ipfsUrl;
        _httpClient = httpClient;
        _ipfsGateway = ipfsGateway;
        _userName = userName;
        _password = password;
    }

    public async Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(MetaAuthMetadata metadata, string fileName)
    {
        var ipfsUploader = new IpfsUploader(_ipfsUrl, _userName, _password, _httpClient);
        var result = await ipfsUploader.AddObjectAsJson(metadata, fileName);
        return result;
    }

    public async Task<MetaAuthMetadata> GetNftMetadataFromIpfsAsync(string cid)
    {
        var ipfsDownloader = new IpfsDownloader(_ipfsGateway, cid, _httpClient);
        var result = await ipfsDownloader.GetAsync();
        return result;
    }
}