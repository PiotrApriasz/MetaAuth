using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace MetaAuth.ContractIntegration.Core.TransactionMethods;

[Function("safeMInt")]
public class SafeMintFunction : FunctionMessage
{
    [Parameter("address", "to", 1)]
    public string To { get; set; }
    
    [Parameter("string", "uri", 2)]
    public string Uri { get; set; }
}