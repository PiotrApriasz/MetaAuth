using MetaAuth.Client.Entities;

namespace MetaAuth.Client.Services;

public interface IAccountService
{
    Task<SignUpData?> GetSignUpData(string requestId);
}