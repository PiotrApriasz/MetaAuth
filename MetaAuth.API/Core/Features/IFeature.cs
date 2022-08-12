namespace MetaAuth.API.Core.Features;

public interface IFeature
{
    IServiceCollection RegisterFeature(IServiceCollection builder);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}