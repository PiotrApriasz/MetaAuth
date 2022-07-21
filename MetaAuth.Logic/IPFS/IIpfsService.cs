using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;

namespace MetaAuth.Logic.IPFS;

public interface IIpfsService
{
    Task<IpfsFileInfo> AddNftMetadataToIpfsAsync(MetaAuthUserData metadata, string fileName);
    Task<MetaAuthUserData> GetNftMetadataFromIpfsAsync(string cid);
}