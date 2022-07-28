using Ipfs.Http;
using MetaAuth.Logic.Entities;
using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;
using MetaAuth.Logic.Exceptions;

namespace MetaAuth.Logic.IPFS;

public class IpfsService : IIpfsService
{
    private readonly string _userName;
    private readonly string _password;
    private readonly string _ipfsUrl;
    private readonly string _ipfsGateway;
    private readonly HttpClient _httpClient;

    public IpfsService(IpfsConfig config, HttpClient httpClient)
    {
        _ipfsUrl = config.IpfsServiceBaseUrl;
        _httpClient = httpClient;
        _ipfsGateway = config.IpfsGateway;
        _userName = config.IpfsProjectId;
        _password = config.IpfsKey;
    }

    public async Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(MetaAuthUserData metadata, string fileName)
    {
        try
        {
            var ipfsUploader = new IpfsUploader(_ipfsUrl, _userName, _password, _httpClient);
            var result = await ipfsUploader.AddObjectAsJson(metadata, fileName);
            return result;
        }
        catch (Exception e)
        {
            throw new IpfsConnectionException
                ($"Error occured while adding file to ipfs service: {e.Message} | {e.StackTrace}");
        }
    }

    public async Task<MetaAuthUserData> GetNftMetadataFromIpfsAsync(string cid)
    {
        try
        {
            var ipfsDownloader = new IpfsDownloader(_ipfsGateway, cid, _httpClient);
            var result = await ipfsDownloader.GetAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new IpfsConnectionException
                ($"Error occured while getting data from ipfs service | {e.Message} | {e.StackTrace}");
        }
    }
}