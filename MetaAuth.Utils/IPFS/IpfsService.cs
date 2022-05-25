using Ipfs.Http;
using MetaAuth.Utils.IPFS.Entities;

namespace MetaAuth.Utils.IPFS;

public class IpfsService<T, TEnum> 
    where TEnum : Enum
    where T : MetaAuthMetadata<TEnum>
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

    public async Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(T metadata, string fileName)
    {
        var ipfsUploader = new IpfsUploader<T, TEnum>(_ipfsUrl, _userName, _password, _httpClient);
        var result = await ipfsUploader.AddObjectAsJson(metadata, fileName);
        return result;
    }

    public async Task<T> GetNftMetadataFromIpfsAsync(string cid)
    {
        var ipfsDownloader = new IpfsDownloader<T, TEnum>(_ipfsGateway, cid, _httpClient);
        var result = await ipfsDownloader.GetAsync();
        return result;
    }
}