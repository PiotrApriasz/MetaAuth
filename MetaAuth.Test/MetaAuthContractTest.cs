using System.Net.Http;
using NBitcoin.Secp256k1;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using MetaAuth.ContractIntegration.Contracts;
using MetaAuth.Metamask.Metamask;
using MetaAuth.Utils.IPFS;
using MetaAuth.Utils.IPFS.Entities;
using Moq;
using Nethereum.Util;
using Xunit;

namespace MetaAuth.Test;

public class MetaAuthContractTest
{
    private Web3 GetWeb3()
    {
        var account = new Account("a8fee21c08c322fde3021cf1bea7b469fa5182bb7186126e803abb98b530e49e");
        var infuraUrl = "https://ropsten.infura.io/v3/c47347be164a4832ac5e7a6e96157dba";
        return new Web3(account, infuraUrl);
    }
    
    private MetaAuthMetadata<MetaAuthType> GetMetadata() =>
        new()
        {
            Type = MetaAuthType.UserData,
            Description = "This is second test ipfs data for testing IpfsService in MetaAuth app",
            Username = "testuser",
            IssueTime = 123456789,
            WebAppAddress = "testTest.com"
        };

    private HttpClient MockHttpClient()
    {
        var mockFactory = new Mock<IHttpClientFactory>();
        var client = new HttpClient();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        
        return mockFactory.Object.CreateClient();
    }

    private IpfsService<MetaAuthMetadata<MetaAuthType>, MetaAuthType> GetIpfsSerrvice() =>
        new ("28k2jK8Gu4h3ZhNbLclNlI0qx81", "c0903b857c2818539f1b27c308fd9bf7",
            "https://ipfs.infura.io:5001", "https://meta-auth.infura-ipfs.io", MockHttpClient());
    
    [Fact]
    public async void SafeMint_CorrectData_MintNftWithMetadata()
    {
        var web3 = GetWeb3();
        web3.Eth.TransactionManager.UseLegacyAsDefault = true;

        var metaAuthContract = new ContractIntegration.Contracts.MetaAuth
            (new MetamaskHostProvider(null), "0x9c3507cbf042f5bd7df1b8fd449ebb31b62ae948");
        
        var ipfsService = GetIpfsSerrvice();
        var returnData = await ipfsService.AddNftMetadataToIpfsAsync(GetMetadata(), "metaauth-9971-testuser.json");

        var mintRecitpt = await metaAuthContract
            .SafeMintRequestAsync("0xF2293B4C787f47726DbAD88d412F8F0cD2A17Fe3", returnData.Hash);

        var ownerOfToken = await metaAuthContract.OwnerOfQueryAsync(0);
        
        Assert.True(ownerOfToken.IsTheSameAddress("0xF2293B4C787f47726DbAD88d412F8F0cD2A17Fe3"));

        var tokenUri = await metaAuthContract.TokenUriQueryAsync(0);
        
        Assert.Equal(returnData.Hash, tokenUri);
        
        await ipfsService.GetNftMetadataFromIpfsAsync(returnData.Hash);

    }
}