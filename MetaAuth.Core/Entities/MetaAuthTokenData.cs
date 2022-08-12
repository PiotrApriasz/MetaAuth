using MetaAuth.Core.Entities.User;
using MetaAuth.Core.Entities.IPFS;

namespace MetaAuth.Core.Entities;

public class MetaAuthTokenData
{
    public int TokenId { get; set; }
    public string OwnerAddress { get; set; }
    public string MetadataCid { get; set; }
    public MetaAuthUserData Metadata { get; set; }

    public MetaAuthTokenData(string ownerAddress, string metadataCid, MetaAuthUserData metadata, int tokenId)
    {
        OwnerAddress = ownerAddress;
        MetadataCid = metadataCid;
        Metadata = metadata;
        TokenId = tokenId;
    }
}