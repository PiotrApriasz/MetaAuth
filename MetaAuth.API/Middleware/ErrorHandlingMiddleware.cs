using System.Net;
using MetaAuth.API.Core.Response;

namespace MetaAuth.API.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            await context.HandleExceptionAsync(e, HttpStatusCode.InternalServerError, "Something went wrong: ");
        }
    }
}