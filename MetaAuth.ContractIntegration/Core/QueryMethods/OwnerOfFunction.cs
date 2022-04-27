using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace MetaAuth.ContractIntegration.Core.QueryMethods;

[Function("ownerOf", "address")]
public class OwnerOfFunction : FunctionMessage
{
    [Parameter("uint256", "_tokenId", 1)]
    public int TokenId { get; set; }
}