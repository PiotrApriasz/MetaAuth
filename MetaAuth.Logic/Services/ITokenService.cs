namespace MetaAuth.Logic.Services;

public interface ITokenService
{
    Task<bool> CheckIfUserHaveTokenAsync(string address);
}