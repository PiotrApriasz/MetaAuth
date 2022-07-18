using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;

namespace MetaAuth.Logic.Entities;

public class MetaAuthMintResult<T>
    where T : MetaAuthUserData
{
    public int TokenId { get; set; }
    public string OwnerAddress { get; set; }
    public string MetadataCid { get; set; }
    public T Metadata { get; set; }

    public MetaAuthMintResult(string ownerAddress, string metadataCid, T metadata, int tokenId)
    {
        OwnerAddress = ownerAddress;
        MetadataCid = metadataCid;
        Metadata = metadata;
        TokenId = tokenId;
    }
}