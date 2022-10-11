using System.Numerics;
using MetaAuth.Core.Entities;
using MetaAuth.Core.Entities.User;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Core.Services;

public interface ITokenService
{
    Task<bool> CheckIfUserHaveTokenAsync(string address);
    Task<bool> CheckTokenPossesion(BigInteger tokenId, string accountAddress);
    Task<MetaAuthTokenData> SafeMint(MetaAuthUserData metadata, byte[] photoBytes, string userMetamaskAddress);
    Task<MetaAuthTokenData?> GetUserToken(int tokenId, string accountAddress);
    Task<bool> AddAppToToken(SignUpModel data, MetaAuthTokenData userTokenData);
}