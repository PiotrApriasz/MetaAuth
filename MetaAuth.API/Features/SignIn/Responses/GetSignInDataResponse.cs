using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.API.Features.SignIn.Responses;

public class GetSignInDataResponse : BaseResponse
{
    public required SignInModel SignInModel { get; set; }
}