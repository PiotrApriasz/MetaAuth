namespace MetaAuth.ContractIntegration.Contracts;

public class MetaAuth : IContract
{
    public string ContractAddress { get; set; }

    public MetaAuth(string contractAddress)
    {
        ContractAddress = contractAddress;
    }
}