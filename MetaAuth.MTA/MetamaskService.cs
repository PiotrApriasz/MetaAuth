using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaAuth.Metamask.Ethereum;

namespace MetaAuth.MTA;

public class MetamaskService : IMetamaskService
{
    public string SelectedAccount { get; set; }
    private readonly IEthereumHostProvider _hostProvider;
    
    public MetamaskService(IEthereumHostProvider hostProvider)
    {
        _hostProvider = hostProvider;
    }

    public async Task<bool> CheckMetamaskAvailable()
    {
        _hostProvider.SelectedAccountChanged += MetamaskHostProvider_SelectedAccountChanged;
        return await _hostProvider.CheckProviderAvailabilityAsync();
    }
    
    public Task MetamaskHostProvider_SelectedAccountChanged(string account)
    {
        SelectedAccount = account;
        return Task.CompletedTask;
    }
    
    public async Task EnableEthereumAsync()
    {
        SelectedAccount = await _hostProvider.EnableProviderAsync();
    }
}

