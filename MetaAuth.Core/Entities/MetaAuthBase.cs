using MetaAuth.ContractIntegration.Contracts;
using Nethereum.Web3;

namespace MetaAuth.Core.Entities;

 public abstract class MetaAuthBase
{
    protected MetaAuthContract MetaAuthInstance { get; set; }

    protected MetaAuthBase(IWeb3 web3, string address)
    {
        MetaAuthInstance = new MetaAuthContract(web3, address);
        MetaAuthInstance.Web3.Eth.TransactionManager.UseLegacyAsDefault = true;
    }
}