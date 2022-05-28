using MetaAuth.ContractIntegration.Contracts;
using MetaAuth.Metamask.Ethereum;

namespace MetaAuth.Utils;

 public abstract class MetaAuthBase
{
    protected MetaAuthContract MetaAuthInstance { get; set; }

    protected MetaAuthBase(IEthereumHostProvider hostProvider)
    {
        MetaAuthInstance = new MetaAuthContract(hostProvider, MetaAuthSettings.SmartContractAddress);
        MetaAuthInstance.Web3.Eth.TransactionManager.UseLegacyAsDefault = true;
    }
}