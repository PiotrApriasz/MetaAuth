using MetaAuth.Logic.Entities.IPFS;

namespace MetaAuth.Logic.IPFS;

public interface IIpfsService<T>
{
    Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(T metadata, string fileName);
    Task<T> GetNftMetadataFromIpfsAsync(string cid);
}