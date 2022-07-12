using Nethereum.Contracts.ContractHandlers;
using Nethereum.Web3;

namespace MetaAuth.ContractIntegration.Contracts;

public interface IContract
{
    public IWeb3 Web3 { get; set; }
    public ContractHandler ContractHandler { get; set; }
}