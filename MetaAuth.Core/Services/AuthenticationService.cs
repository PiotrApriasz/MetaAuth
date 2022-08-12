using System.Numerics;
using MetaAuth.Core.Entities;
using MetaAuth.Core.IPFS;
using MetaAuth.Core.Entities.IPFS;
using MetaAuth.Core.Entities.User;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ERC721.ContractDefinition;
using Nethereum.UI;
using Nethereum.Web3;

namespace MetaAuth.Core.Services;

public class AuthenticationService : MetaAuthBase, IAuthenticationService
{
    private readonly IIpfsService _ipfsService;

    public AuthenticationService(IWeb3 hostProvider, string address, IIpfsService ipfsService) : base(hostProvider, address)
    {
        _ipfsService = ipfsService;
    }

    /*public async Task<T> GetUserData(string cid) => await _ipfsService.GetNftMetadataFromIpfsAsync(cid);

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
    }*/
}