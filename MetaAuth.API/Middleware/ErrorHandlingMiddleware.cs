using System.Net;
using MetaAuth.API.Core.Response;
using Microsoft.Azure.Cosmos;

namespace MetaAuth.API.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (CosmosException e)
        {
            await context.HandleExceptionAsync(e, HttpStatusCode.InternalServerError, "Error occured during " +
                "connection with database: ");
        }
        catch (Exception e)
        {
            await context.HandleExceptionAsync(e, HttpStatusCode.InternalServerError, "Something went wrong: ");
        }
    }
}