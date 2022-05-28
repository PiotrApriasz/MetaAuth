using Newtonsoft.Json;

namespace MetaAuth.Utils.IPFS.Entities;

/// <summary>
/// Base class for storing user data on IPFS server.
/// Use this as a base class for class with other properties
/// you need to store on ipfs. Your class needs to be extended
/// with this class because generics methods only allows
/// types with this class as base.
/// </summary>
public class MetaAuthMetadata
{
    /// <summary>
    /// Specify type of MetaAuth token
    /// </summary>
    [JsonProperty("type")]
    public MetaAuthType Type { get; set; }
    
    /// <summary>
    /// Description to better understand what is specific token for
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// The most important part of token. This allows service
    /// to recognize and confirm that token is connected with specific app
    /// </summary>
    [JsonProperty("appAddress")]
    public string WebAppAddress { get; set; }
    
    /// <summary>
    /// User username who choose this way to register
    /// </summary>
    [JsonProperty("username")]
    public string Username { get; set; }
    
    /// <summary>
    /// Guid which helps to navigate through standard data base
    /// in case of search others user data
    /// </summary>
    [JsonProperty("guid")]
    public string Guid { get; set; }
    
    /// <summary>
    /// Time when token has been generated
    /// </summary>
    [JsonProperty("issue_time")]
    public uint IssueTime { get; set; }
}