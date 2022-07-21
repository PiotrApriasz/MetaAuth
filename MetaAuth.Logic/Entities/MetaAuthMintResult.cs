using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;

namespace MetaAuth.Logic.Entities;

public class MetaAuthMintResult
{
    public int TokenId { get; set; }
    public string OwnerAddress { get; set; }
    public string MetadataCid { get; set; }
    public MetaAuthUserData Metadata { get; set; }

    public MetaAuthMintResult(string ownerAddress, string metadataCid, MetaAuthUserData metadata, int tokenId)
    {
        OwnerAddress = ownerAddress;
        MetadataCid = metadataCid;
        Metadata = metadata;
        TokenId = tokenId;
    }
}