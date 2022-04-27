using Nethereum.ABI.FunctionEncoding.Attributes;

namespace MetaAuth.ContractIntegration.Core.QueryMethods;

[Function("balanceOf", "uint256")]
public class BalanceOfFunction
{
    [Parameter("address", "_owner", 1)]
    public virtual string Owner { get; set; }
}