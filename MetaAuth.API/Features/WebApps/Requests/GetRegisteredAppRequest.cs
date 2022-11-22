using MetaAuth.API.Core.Endpoints;

namespace MetaAuth.API.Features.WebApps.Requests;

public class GetRegisteredAppRequest : IHttpRequest
{
    public required string WebAppAddress { get; set; }
}