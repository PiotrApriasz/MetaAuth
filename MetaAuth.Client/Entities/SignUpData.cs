using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Client.Entities;

public class SignUpData : BaseResponse
{
    public SignUpModel SignUpModel { get; set; }
}