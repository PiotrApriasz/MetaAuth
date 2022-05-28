using System.Net.Http;
using MetaAuth.Utils.IPFS;
using MetaAuth.Utils.IPFS.Entities;
using Moq;
using Xunit;

namespace MetaAuth.Test;

public class IpfsServiceTest
{
    private static MetaAuthMetadata GetMetadata() =>
        new()
        {
            Type = MetaAuthType.UserData,
            Description = "This is test ipfs data for testing IpfsService in MetaAuth app",
            Username = "testuser",
            IssueTime = 123456,
            WebAppAddress = "test.com"
        };

    private HttpClient MockHttpClient()
    {
        var mockFactory = new Mock<IHttpClientFactory>();
        var client = new HttpClient();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        
        return mockFactory.Object.CreateClient();
    }

    private IpfsService<MetaAuthMetadata> GetIpfsSerrvice() =>
        new("28k2jK8Gu4h3ZhNbLclNlI0qx81", "c0903b857c2818539f1b27c308fd9bf7",
            "https://ipfs.infura.io:5001", "https://meta-auth.infura-ipfs.io", MockHttpClient());

    [Fact]
    public async void AddNftMetadataToIpfsAsync_CorrectMetaData_ReturnsIpfsFileInfo()
    {
        var ipfsService = GetIpfsSerrvice();
        await ipfsService.AddNftMetadataToIpfsAsync(GetMetadata(), "metaauth-9971-testuser.json");
    }

    [Fact]
    public async void GetNftMetadataFromIpfsAsync_CorrectCid_ReturnsMetadata()
    {
        var ipfsService = GetIpfsSerrvice();
        await ipfsService.GetNftMetadataFromIpfsAsync("QmPiBx6QN2u29td2H7Q4WbtdcTZoPYdZZzREvix19x1Djc");
    }
}