using System.Net.Http.Json;
using System.Numerics;
using MetaAuth.Core.Entities;
using MetaAuth.Core.Entities.User;
using MetaAuth.Core.Exceptions;
using MetaAuth.Core.IPFS;
using MetaAuth.SharedEntities.AzureCosmosDb;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ERC721.ContractDefinition;
using Nethereum.Web3;

namespace MetaAuth.Core.Services;

public class TokenService : MetaAuthBase, ITokenService
{
    private readonly IIpfsService _ipfsService;
    private readonly IJwtService _jwtService;

    public TokenService(IWeb3 web3, string address, IIpfsService ipfsService,
        IJwtService jwtService) : base(web3, address)
    {
        _ipfsService = ipfsService;
        _jwtService = jwtService;
    }
    
    public async Task<MetaAuthTokenData> SafeMint(MetaAuthUserData metadata, byte[] photoBytes, string userMetamaskAddress)
    {
        var ipfsFileInfo = await _ipfsService.AddNftMetadataToIpfsAsync(metadata, 
            $"metaauth-{metadata.IssueTime}-{metadata.Name}{metadata.Surname}.json");

        var mintReceipt = await MetaAuthInstance.SafeMintRequestAsync(userMetamaskAddress,
            ipfsFileInfo.Hash);

        var userData = await _ipfsService.GetNftMetadataFromIpfsAsync(ipfsFileInfo.Hash);

        var transferEvent = mintReceipt.DecodeAllEvents<TransferEventDTO>()[0];
        var tokenId = (int)transferEvent.Event.TokenId;
        var owner = transferEvent.Event.To;
        
        return new MetaAuthTokenData(owner, ipfsFileInfo.Hash, userData, tokenId);

    }
    
    public async Task<bool> CheckIfUserHaveTokenAsync(string address)
    {
        var tokenCount = await MetaAuthInstance.BalanceOfQueryAsync(address);
        return tokenCount != 0;
    }
    
    public async Task<bool> CheckTokenPossesion(BigInteger tokenId, string accountAddress)
    {
        try
        {
            var tokenOwnerById = await MetaAuthInstance.OwnerOfQueryAsync(tokenId);
            return string.Equals(tokenOwnerById, accountAddress, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception e)
        {
            if (e.Message == "execution reverted: ERC721: invalid token ID")
                return false;

            throw;
        }
    }

    public async Task<MetaAuthTokenData?> GetUserToken(int tokenId, string accountAddress)
    {
        var tokenIdNum = new BigInteger(tokenId);

        var result = await CheckTokenPossesion(tokenIdNum, accountAddress);

        if (!result)
            return null;

        var cid = await MetaAuthInstance.TokenUriQueryAsync(tokenIdNum);

        var metadata = await _ipfsService.GetNftMetadataFromIpfsAsync(cid);

        return new MetaAuthTokenData(accountAddress, cid, metadata, (int)tokenIdNum);
    }
    
    public async Task<bool> AddAppToToken(SignUpModel data, MetaAuthTokenData userTokenData)
    {
        var tokenIdNum = new BigInteger(userTokenData.TokenId);
        
        userTokenData.Metadata.RegisteredApps.Add(new RegisteredApp
        {
            UserId = data.UserIdentificator,
            WebAppAddress = data.AppName,
            RegisterTime = data.RequestCreation,
            RequiredUserData = data.RequiredUserData
        });
        
        userTokenData.Metadata.IssueTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        var ipfsFileInfo = await _ipfsService.AddNftMetadataToIpfsAsync(userTokenData.Metadata,
            $"metaauth-{userTokenData.Metadata.IssueTime}-{userTokenData.Metadata.Name}{userTokenData.Metadata.Surname}.json");

        return await MetaAuthInstance.UpdateTokenUriRequestAsync(tokenIdNum, ipfsFileInfo.Hash);
    }

    public string? ValidateToken(string webAppAddress, MetaAuthTokenData userTokenData, RegisteredWebAppsModel webApp)
    {
        var thisApp = userTokenData.Metadata.RegisteredApps.Find(x => x.WebAppAddress == webAppAddress);

        if (thisApp is null)
            return null;

        var userData = new Dictionary<string, string> { { "id", thisApp.UserId } };

        foreach (var userDataType in thisApp.RequiredUserData)
        {
            if (userDataType == "username") continue;
            
            userData.Add(userDataType, 
                userTokenData.Metadata.GetType().GetProperty(Capitalize(userDataType))!.GetValue(userTokenData.Metadata, null)!.ToString()!);
        }

        var token = _jwtService.GenerateToken(userData, webApp);

        return token;
    }

    private static string Capitalize( string name)
    {
        return char.ToUpper(name[0]) + name[1..];
    }
}