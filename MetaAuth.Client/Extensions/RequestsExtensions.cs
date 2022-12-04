using System.Net.Http.Json;
using MetaAuth.Client.Entities.Exceptions;
using MetaAuth.SharedEntities;
using Microsoft.IdentityModel.Tokens;

namespace MetaAuth.Client.Extensions;

public static class RequestsExtensions
{
    public static async Task<T> ProccessGetResult<T>(this HttpResponseMessage result) where T : BaseResponse
    {
        var model = await result.Content.ReadFromJsonAsync<T>();
        
        if (model is null)
            throw new MetaAuthApiException("There is now response from MetaAuth API");
        
        if (model.Error)
            throw new MetaAuthApiException(model.Message);

        return model;
    }
    
    public static async Task ProccessPostResult(this HttpResponseMessage result)
    {
        var model = await result.Content.ReadFromJsonAsync<BaseResponse>();
        
        if (model is null)
            throw new MetaAuthApiException("There is now response from MetaAuth API");
        
        if (model.Error)
            throw new MetaAuthApiException(model.Message);
    }
}