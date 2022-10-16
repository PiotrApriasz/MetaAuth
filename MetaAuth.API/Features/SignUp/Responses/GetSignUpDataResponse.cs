
using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.API.Features.SignUp.Responses;

public class GetSignUpDataResponse : BaseResponse
{
    public SignUpModel SignUpModel { get; set; } = null!;
}