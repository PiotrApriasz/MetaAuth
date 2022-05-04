using Nethereum.Signer;

namespace MetaAuth.Metamask.Ethereum;

public class NethereumAuthenticator
{
    private readonly IEthereumHostProvider _host;
    private string _currentChallenge = null!;
    private const string Prefix = "Please sign this one time message to authenticate";

    public NethereumAuthenticator(IEthereumHostProvider host)
    {
        _host = host;
    }
    
    public string GetNewChallenge(string? message = null)
    {
        message ??= Prefix;
        var currentChallenge = DateTime.Now.ToString("O") + "- Challenge";
        var key = EthECKey.GenerateKey();
        var currentKey = key.GetPrivateKey();
        var signer = new MessageSigner();
        _currentChallenge =  signer.HashAndSign(currentChallenge, currentKey);
        return message + _currentChallenge;
    }
    
    public async Task<string> RequestNewChallengeSignatureAndRecoverAccountAsync(string message = null)
    {
        if (!_host.Available)
        {
            throw new Exception("Cannot authenticate user, an Ethereum host is not available");
        }

        var challenge = GetNewChallenge(message);
        var signedMessage = await _host.SignMessageAsync(challenge);

        return new EthereumMessageSigner().EncodeUTF8AndEcRecover(challenge, signedMessage);
    }
}