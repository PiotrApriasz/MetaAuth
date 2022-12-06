using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.API.Features.SignIn.Services;

public interface ISignInService
{
    Task<string> RegisterMetaAuthSignIn(InitialSignInRequest request);
    Task<SignInModel?> GetSignInData(GetSignInDataRequest request);
    Task FinishSignIn(FinishSignInRequest request);
    Task<string?> GetAccessToken(GetJwtTokenRequest request);
}