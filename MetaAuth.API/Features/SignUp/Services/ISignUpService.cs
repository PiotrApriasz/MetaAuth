using MetaAuth.API.Features.SignUp.Requests;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.API.Features.SignUp.Services;

public interface ISignUpService
{
    Task<string> RegisterMetaAuthSignUp(InitialSignUpRequest request);
    Task<SignUpModel?> GetSignUpData(GetSignUpDataRequest request);
}