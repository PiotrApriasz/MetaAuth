using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace MetaAuth.ContractIntegration.Core.TransactionMethods;

[Function("updateTokenURI")]
public class UpdateTokenURIFunction : FunctionMessage
{
    [Parameter("uint256", "tokenId")]
    public BigInteger TokenId { get; set; }
    
    [Parameter("string", "uri", 2)]
    public string Uri { get; set; }
}