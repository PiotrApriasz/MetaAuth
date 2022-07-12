using MetaAuth.ContractIntegration.Contracts;
using MetaAuth.Metamask.Ethereum;
using Nethereum.UI;
using Nethereum.Web3;

namespace MetaAuth.Logic;

 public abstract class MetaAuthBase
{
    protected MetaAuthContract MetaAuthInstance { get; set; }

    protected MetaAuthBase(IWeb3 web3, string address)
    {
        MetaAuthInstance = new MetaAuthContract(web3, address);
        MetaAuthInstance.Web3.Eth.TransactionManager.UseLegacyAsDefault = true;
    }
}