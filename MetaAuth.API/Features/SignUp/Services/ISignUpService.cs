using MetaAuth.API.Features.SignUp.Requests;

namespace MetaAuth.API.Features.SignUp.Services;

public interface ISignUpService
{
    Task<string> RegisterMetaAuthSignUp(InitialSignUpRequest request);
}