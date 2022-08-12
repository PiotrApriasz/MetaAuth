using MetaAuth.Core.Entities.IPFS;
using MetaAuth.Core.Entities.User;

namespace MetaAuth.Core.IPFS;

public interface IIpfsService
{
    Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(MetaAuthUserData metadata, string fileName);
    Task<MetaAuthUserData> GetNftMetadataFromIpfsAsync(string cid);
}