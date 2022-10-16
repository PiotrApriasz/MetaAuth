using System.Numerics;
using MetaAuth.ContractIntegration.Core.QueryMethods;
using MetaAuth.ContractIntegration.Core.TransactionMethods;
using MetaAuth.ContractIntegration.Exceptions;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace MetaAuth.ContractIntegration.Contracts;

public class MetaAuthContract : IContract
{
    public IWeb3 Web3 { get; set; }
    public ContractHandler ContractHandler { get; set; }
    

    public MetaAuthContract(IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = Web3.Eth.GetContractHandler(contractAddress);
    }

    public async Task<BigInteger> BalanceOfQueryAsync(string owner)
    {
        var balanceOfFunction = new BalanceOfFunction
        {
            Owner = owner
        };

        return await ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction);
    }

    public async Task<string> OwnerOfQueryAsync(BigInteger tokenId)
    {
        var ownerOfFunction = new OwnerOfFunction()
        {
            TokenId = tokenId
        };

        return await ContractHandler.QueryAsync<OwnerOfFunction, string>(ownerOfFunction);
    }

    public async Task<string> TokenUriQueryAsync(BigInteger tokenId)
    {
        var tokenUriFunction = new TokenUriFunction()
        {
            TokenId = tokenId
        };

        return await ContractHandler.QueryAsync<TokenUriFunction, string>(tokenUriFunction);
    }

    public async Task<string> BurnRequestAsync(BigInteger tokenId)
    {
        var burnFunction = new BurnFunction()
        {
            TokenId = tokenId
        };

        return await ContractHandler.SendRequestAsync(burnFunction);
    }
    
    public async Task<string> PauseRequestAsync()
    {
        var pauseFunction = new PauseFunction();
        return await ContractHandler.SendRequestAsync(pauseFunction);
    }
    
    public async Task<string> UnPauseRequestAsync()
    {
        var unpauseFunction = new UnpauseFunction();
        return await ContractHandler.SendRequestAsync(unpauseFunction);
    }

    public async Task<TransactionReceipt> SafeMintRequestAsync(string to, string uri)
    {
        try
        {
            var safeMintFunction = new SafeMintFunction()
            {
                To = to,
                Uri = uri
            };

            //safeMintFunction.Gas = await ContractHandler.EstimateGasAsync(safeMintFunction);
            //safeMintFunction.GasPrice = Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei);

            //safeMintFunction.Gas = 3000000;
            //safeMintFunction.GasPrice = 10000000000;

            return await ContractHandler.SendRequestAndWaitForReceiptAsync(safeMintFunction);
        }
        catch (Exception e)
        {
            throw new ContractException($"Error occured while minting NFT | {e.Message} | {e.StackTrace}");
        }
    }

    public async Task<bool> UpdateTokenUriRequestAsync(BigInteger tokenId, string uri)
    {
        var updateTokenUriFunction = new UpdateTokenURIFunction
        {
            TokenId = tokenId,
            Uri = uri
        };

        try
        {
            await ContractHandler.SendRequestAsync(updateTokenUriFunction);
            return true;
        }
        catch (RpcResponseException e)
        {
            if (e.Message.Contains("User denied transaction signature"))
            {
                return false;
            }
            
            throw new ContractException($"Error occured while updating token uri | {e.Message} | {e.StackTrace}");
        }
        catch (Exception e)
        {
            throw new ContractException($"Error occured while updating token uri | {e.Message} | {e.StackTrace}");
        }
    }
}