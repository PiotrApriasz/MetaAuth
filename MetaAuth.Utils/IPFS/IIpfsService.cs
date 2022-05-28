using MetaAuth.Utils.IPFS.Entities;

namespace MetaAuth.Utils.IPFS;

public interface IIpfsService<T>
{
    Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(T metadata, string fileName);
    Task<T> GetNftMetadataFromIpfsAsync(string cid);
}