using FluentValidation;
using MetaAuth.API.Core.Response;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Filters;

public class ValidatorFilter<T> : IEndpointFilter
{
    private readonly IValidator<T> _validator;

    public ValidatorFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.Arguments.SingleOrDefault(x => x?.GetType() == typeof(T)) is not T validatable)
        {
            return Results.BadRequest();
        }

        var validationResult = await _validator.ValidateAsync(validatable);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(new BaseResponse()
            {
                Error = true,
                Message = validationResult.Errors.ToResponse()
            });
        }

        return await next(context);
    }
}