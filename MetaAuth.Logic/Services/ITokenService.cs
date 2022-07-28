using System.Numerics;
using MetaAuth.Logic.Entities;
using MetaAuth.Logic.Entities.User;

namespace MetaAuth.Logic.Services;

public interface ITokenService
{
    Task<bool> CheckIfUserHaveTokenAsync(string address);
    Task<bool> CheckTokenPossesion(BigInteger tokenId, string accountAddress);
    Task<MetaAuthTokenData> SafeMint(MetaAuthUserData metadata, byte[] photoBytes, string userMetamaskAddress);
    Task<MetaAuthTokenData?> GetUserToken(int tokenId, string accountAddress);
}