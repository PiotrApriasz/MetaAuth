using System.Net;
using FluentValidation.Results;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Core.Response;

public static class ResponseTools
{
    public static string ToResponse(this IEnumerable<ValidationFailure> errors)
    {
        return string.Join(", ", errors);
    }

    public static async Task HandleExceptionAsync(this HttpContext context, Exception exception, HttpStatusCode statusCode,
        string message = "")
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(new BaseResponse()
        {
            Error = true,
            Message = message + exception.Message /*+ exception.StackTrace*/ // StackTrace only for testing purposes
        });
    }
}