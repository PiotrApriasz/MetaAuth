using MetaAuth.Logic.Entities;
using MetaAuth.Logic.Entities.User;

namespace MetaAuth.Logic.Services;

public interface IAuthenticationService
{
    Task<MetaAuthMintResult> SafeMint(MetaAuthUserData metadata, byte[] photoBytes, string userMetamaskAddress);
}