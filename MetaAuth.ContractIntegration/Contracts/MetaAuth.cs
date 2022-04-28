using System.Numerics;
using MetaAuth.ContractIntegration.Core.QueryMethods;
using MetaAuth.ContractIntegration.Core.TransactionMethods;
using MetaAuth.Metamask.Ethereum;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Web3;

namespace MetaAuth.ContractIntegration.Contracts;

public class MetaAuth : IContract
{
    public Web3 Web3 { get; set; }
    public ContractHandler ContractHandler { get; set; }

    public MetaAuth(IEthereumHostProvider hostProvider, string contractAddress)
    {
        Web3 = hostProvider.GetWeb3();
        ContractHandler = Web3.Eth.GetContractHandler(contractAddress);
    }

    public Task<BigInteger> BalanceOfQueryAsync(string owner)
    {
        var balanceOfFunction = new BalanceOfFunction
        {
            Owner = owner
        };

        return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction);
    }

    public Task<string> OwnerOfQueryAsync(BigInteger tokenId)
    {
        var ownerOfFunction = new OwnerOfFunction()
        {
            TokenId = tokenId
        };

        return ContractHandler.QueryAsync<OwnerOfFunction, string>(ownerOfFunction);
    }

    public Task<string> TokenUriQueryAsync(BigInteger tokenId)
    {
        var tokenUriFunction = new TokenUriFunction()
        {
            TokenId = tokenId
        };

        return ContractHandler.QueryAsync<TokenUriFunction, string>(tokenUriFunction);
    }

    public Task<string> BurnRequestAsync(BigInteger tokenId)
    {
        var burnFunction = new BurnFunction()
        {
            TokenId = tokenId
        };

        return ContractHandler.SendRequestAsync(burnFunction);
    }

    public Task<string> PauseRequestAsync()
    {
        var pauseFunction = new PauseFunction();
        return ContractHandler.SendRequestAsync(pauseFunction);
    }
    
    public Task<string> UnPauseRequestAsync()
    {
        var unpauseFunction = new UnpauseFunction();
        return ContractHandler.SendRequestAsync(unpauseFunction);
    }

    public Task<string> SafeMintRequestAsync(string to, string uri)
    {
        var safeMintFunction = new SafeMintFunction()
        {
            To = to,
            Uri = uri
        };

        return ContractHandler.SendRequestAsync(safeMintFunction);
    }
}