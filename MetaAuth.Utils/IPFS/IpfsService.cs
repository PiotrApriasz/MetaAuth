using Ipfs.Http;

namespace MetaAuth.Utils.IPFS;

public class IpfsService
{
    private readonly string? _userName;
    private readonly string? _password;
    private readonly string _ipfsUrl;

    public IpfsService(string ipfsUrl)
    {
        _ipfsUrl = ipfsUrl;
    }

    public IpfsService(string userName, string password, string ipfsUrl) : this(ipfsUrl)
    {
        _userName = userName;
        _password = password;
    }
}