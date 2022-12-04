using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Client.Entities;

public class SignInData : BaseResponse
{
    public required SignInModel SignInModel { get; set; }
}