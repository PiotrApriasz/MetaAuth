using Newtonsoft.Json;

namespace MetaAuth.SharedEntities;

public class AzCosmosContainerModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public override string ToString() => JsonConvert.SerializeObject(this);
}