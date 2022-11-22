using MediatR;
using MetaAuth.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MetaAuth.API.Core.Endpoints;

public static class EndpointHandlers
{
    public static IEndpointRouteBuilder MapGet<TRequest>(this IEndpointRouteBuilder app, string template
        , bool validatable = true)
        where TRequest : IHttpRequest
    {
        if (validatable)
        {
            app.MapGet(template, async (IMediator mediator, [AsParameters] TRequest request)
                    => await mediator.Send(request))
                .AddEndpointFilter<ValidatorFilter<TRequest>>();
        }
        else
        {
            app.MapGet(template, async (IMediator mediator, [AsParameters] TRequest request)
                => await mediator.Send(request));
        }
        
        return app;
    }
    
    public static IEndpointRouteBuilder MapPost<TRequest>(this IEndpointRouteBuilder app, string template,
        bool validatable = true)
        where TRequest : IHttpRequest
    {
        if (validatable)
        {
            app.MapPost(template, async (IMediator mediator, TRequest request)
                    => await mediator.Send(request))
                .AddEndpointFilter<ValidatorFilter<TRequest>>();
        }
        else
        {
            app.MapPost(template, async (IMediator mediator, TRequest request)
                => await mediator.Send(request));
        }
        
        return app;
    }
}