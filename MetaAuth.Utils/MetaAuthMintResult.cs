using MetaAuth.Utils.IPFS.Entities;

namespace MetaAuth.Utils;

public class MetaAuthMintResult<T>
    where T : MetaAuthMetadata
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