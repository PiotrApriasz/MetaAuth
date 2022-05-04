using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

namespace MetaAuth.Metamask.Metamask;

public class MetamaskRpcRequestMessage : RpcRequestMessage
{
    [JsonProperty("from")]
    public string From { get; private set; }
    
    public MetamaskRpcRequestMessage(object id, string method, string from, params object[] parameterList) : base(id, method,
        parameterList)
    {
        From = from;
    }
}