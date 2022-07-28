using System;
using System.Net.Http;
using NBitcoin.Secp256k1;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using MetaAuth.ContractIntegration.Contracts;
using MetaAuth.Logic.Entities;
using MetaAuth.Logic.Entities.IPFS;
using MetaAuth.Logic.Entities.User;
using MetaAuth.Logic.IPFS;
using Moq;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ERC721.ContractDefinition;
using Nethereum.Util;
using Xunit;
using Xunit.Abstractions;

namespace MetaAuth.Test;

public class MetaAuthContractTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MetaAuthContractTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private Web3 GetWeb3()
    {
        var account = new Account("a8fee21c08c322fde3021cf1bea7b469fa5182bb7186126e803abb98b530e49e");
        const string infuraUrl = "https://goerli.infura.io/v3/272d6a9003e64778a885d5e058ed852a";
        return new Web3(account, infuraUrl);
    }
    
    private MetaAuthUserData GetMetadata() =>
        new()
        {
            
        };

    private HttpClient MockHttpClient()
    {
        var mockFactory = new Mock<IHttpClientFactory>();
        var client = new HttpClient();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        
        return mockFactory.Object.CreateClient();
    }

    [Fact]
    public async void SafeMint_CorrectData_MintNftWithMetadata()
    {
        var web3 = GetWeb3();
        web3.Eth.TransactionManager.UseLegacyAsDefault = true;

        var metaAuthContract = new MetaAuthContract
            (web3, "0x967dbc61c8875e26c74f80ecb7bdd6155fc7724f");
        

        var mintRecitpt = await metaAuthContract
            .SafeMintRequestAsync("0xF2293B4C787f47726DbAD88d412F8F0cD2A17Fe3",
                "bafybeifrxhvfjcixylw3i6clo2xsyugph6d4hploynee6pyta5etaw5xx4");

        var transferEvent = mintRecitpt.DecodeAllEvents<TransferEventDTO>();
        /*var tokenId = (int)transferEvent.Event.TokenId;
        var owner = transferEvent.Event.To;

        _testOutputHelper.WriteLine(tokenId.ToString());

        var ownerOfToken = await metaAuthContract.OwnerOfQueryAsync(tokenId);
        
        Assert.True(ownerOfToken.IsTheSameAddress("0xF2293B4C787f47726DbAD88d412F8F0cD2A17Fe3"));

        var tokenUri = await metaAuthContract.TokenUriQueryAsync(tokenId);*/

    }

    [Fact]
    public async void BalanceOf_NFTMinted_ReturnOne()
    {
        var web3 = GetWeb3();
        web3.Eth.TransactionManager.UseLegacyAsDefault = true;
        
        var metaAuthContract = new MetaAuthContract
            (web3, "0x967dbc61c8875e26c74f80ecb7bdd6155fc7724f");

        var result = await metaAuthContract.BalanceOfQueryAsync("0xf2293b4c787f47726dbad88d412f8f0cd2a17fe3");
        
        Assert.Equal(1, result);
    }
}