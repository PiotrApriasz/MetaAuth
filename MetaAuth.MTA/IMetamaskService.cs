namespace MetaAuth.MTA;

public interface IMetamaskService
{
    string SelectedAccount { get; set; }
    Task<bool> CheckMetamaskAvailable();
    Task MetamaskHostProvider_SelectedAccountChanged(string account);
    Task EnableEthereumAsync();
}