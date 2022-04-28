using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace MetaAuth.ContractIntegration.Core.TransactionMethods;

[Function("_burn")]
public class BurnFunction : FunctionMessage
{
    [Parameter("uint256", "tokenId")]
    public BigInteger TokenId { get; set; }
}