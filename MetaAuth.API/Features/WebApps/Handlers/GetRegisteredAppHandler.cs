using MediatR;
using MetaAuth.API.Features.WebApps.Requests;
using MetaAuth.API.Features.WebApps.Responses;
using MetaAuth.API.Features.WebApps.Services;
using MetaAuth.SharedEntities;

namespace MetaAuth.API.Features.WebApps.Handlers;

public class GetRegisteredAppHandler : IRequestHandler<GetRegisteredAppRequest, IResult>
{
    private readonly IWebAppsService _webAppsService;

    public GetRegisteredAppHandler(IWebAppsService webAppsService)
    {
        _webAppsService = webAppsService;
    }

    public async Task<IResult> Handle(GetRegisteredAppRequest request, CancellationToken cancellationToken)
    {
        var result = await _webAppsService.GetRegisteredApp(request);

        if (result is not null)
        {
            return Results.Ok(new GetRegisteredAppResponse()
            {
                Error = false,
                Message = "Registered app data has been successfully gathered from database",
                RegisteredWebAppsModel = result
            });
        }
        
        return Results.NotFound(new BaseResponse
        {
            Error = true,
            Message = "Registered app data cannot be found for provided request id"
        });
    }
}