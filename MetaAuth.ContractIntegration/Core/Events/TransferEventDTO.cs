using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace MetaAuth.ContractIntegration.Core.Events;

[Event("Transfer")]
public class TransferEventDto : IEventDTO
{
    [Parameter("address", "_from", 1, true )]
    public string From { get; set; } = null!;
    [Parameter("address", "_to", 2, true )]
    public string To { get; set; } = null!;
    [Parameter("uint256", "_tokenId", 3, true )]
    public BigInteger TokenId { get; set; }
}