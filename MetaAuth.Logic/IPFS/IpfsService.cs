using Ipfs.Http;
using MetaAuth.Logic.Exceptions;
using MetaAuth.Logic.IPFS.Entities;

namespace MetaAuth.Logic.IPFS;

public class IpfsService<T> : IIpfsService<T>
    where T : MetaAuthMetadata
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
        try
        {
            var ipfsUploader = new IpfsUploader<T>(_ipfsUrl, _userName, _password, _httpClient);
            var result = await ipfsUploader.AddObjectAsJson(metadata, fileName);
            return result;
        }
        catch (Exception e)
        {
            throw new IpfsConnectionException
                ($"Error occured while adding file to ipfs service | {e.Message} | {e.StackTrace}");
        }
    }

    public async Task<T> GetNftMetadataFromIpfsAsync(string cid)
    {
        try
        {
            var ipfsDownloader = new IpfsDownloader<T>(_ipfsGateway, cid, _httpClient);
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