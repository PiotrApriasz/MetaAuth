using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace MetaAuth.ContractIntegration.Core.QueryMethods;

[Function("tokenURI", "string")]
public class TokenUriFunction : FunctionMessage
{
    [Parameter("uint256", "tokenId", 1)]
    public int TokenId { get; set; }
}