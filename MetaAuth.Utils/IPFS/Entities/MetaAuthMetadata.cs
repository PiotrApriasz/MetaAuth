using Newtonsoft.Json;

namespace MetaAuth.Utils.IPFS.Entities;

public class MetaAuthMetadata
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("appAddress")]
    public string WebAppAddress { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("issueTime")]
    public uint IssueTime { get; set; }
}