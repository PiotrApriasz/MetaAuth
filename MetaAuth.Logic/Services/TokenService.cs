using MetaAuth.Logic.Entities;
using Nethereum.Web3;

namespace MetaAuth.Logic.Services;

public class TokenService : MetaAuthBase, ITokenService
{
    public TokenService(IWeb3 web3, string address) : base(web3, address)
    {
    }
    
    public async Task<bool> CheckIfUserHaveTokenAsync(string address)
    {
        var tokenCount = await MetaAuthInstance.BalanceOfQueryAsync(address);

        return !tokenCount.IsZero;
    }
}