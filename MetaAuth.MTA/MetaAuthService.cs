using MetaAuth.Metamask.Ethereum;
using MetaAuth.Utils;
using MetaAuth.Utils.Exceptions;
using MetaAuth.Utils.IPFS;
using MetaAuth.Utils.IPFS.Entities;
using Nethereum.Contracts;
using Nethereum.StandardNonFungibleTokenERC721.ContractDefinition;

namespace MetaAuth.MTA;

public class MetaAuthService<T> : MetaAuthBase
    where T : MetaAuthMetadata
{
    private readonly IpfsService<T> _ipfsService;
    private readonly NethereumAuthenticator _nethereumAuthenticator;
    
    public MetaAuthService(IEthereumHostProvider hostProvider, HttpClient httpClient) : base(hostProvider)
    {
        _ipfsService = new IpfsService<T>(MetaAuthSettings.IpfsUsername, MetaAuthSettings.IpfsPassword,
            MetaAuthSettings.IpfsServiceBaseUrl, MetaAuthSettings.IpfsGateway, httpClient);
        _nethereumAuthenticator = new NethereumAuthenticator(hostProvider);
    }

    public async Task<MetaAuthMintResult<T>> SafeMint(T metadata, string userMetamaskAddress)
    {
        var ipfsFileInfo = await _ipfsService.AddNftMetadataToIpfsAsync(metadata, 
            $"{MetaAuthSettings.MetadataBaseName}{metadata.Guid}-{metadata.Username}.json");

        var mintReceipt = await MetaAuthInstance.SafeMintRequestAsync(userMetamaskAddress,
            ipfsFileInfo.Hash);

        var userData = await _ipfsService.GetNftMetadataFromIpfsAsync(ipfsFileInfo.Hash);

        var transferEvent = mintReceipt.DecodeAllEvents<TransferEventDTO>()[0];
        var tokenId = (int)transferEvent.Event.TokenId;
        var owner = transferEvent.Event.To;

        return new MetaAuthMintResult<T>(owner, ipfsFileInfo.Hash, userData, tokenId);

    }

    public async Task<T> GetUserData(string cid) => await _ipfsService.GetNftMetadataFromIpfsAsync(cid);

    public async Task<bool> SignIn(int tokenId, string connectedUser,  string webAppAddress)
    {
        var authenticatedAccount = await _nethereumAuthenticator.RequestNewChallengeSignatureAndRecoverAccountAsync();
        if (connectedUser != authenticatedAccount)
            throw new MetaAuthAuthenticationException("Connected account is different from the confirmed one!");
        
        var ownerOfToken = await MetaAuthInstance.OwnerOfQueryAsync(tokenId);
        if (ownerOfToken != authenticatedAccount)
            throw new MetaAuthAuthenticationException("Authenticated user don't have MetaAuth token with provided id");

        var cid = await MetaAuthInstance.TokenUriQueryAsync(tokenId);
        var userData = await GetUserData(cid);

        if (userData.Type != MetaAuthType.UserData)
            throw new MetaAuthAuthenticationException("This MetaAuth token is not for sign in purposes");

        if (userData.WebAppAddress != webAppAddress)
            throw new MetaAuthAuthenticationException("This MetaAuth token is not connected with this web app");

        return true;
    }
}