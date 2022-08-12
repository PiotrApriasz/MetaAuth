namespace MetaAuth.API.Core.Features;

public static class FeatureExtensions
{
    private static readonly List<IFeature> RegisteredFeatures = new();
    
    public static IServiceCollection RegisterFeatures(this IServiceCollection services)
    {
        var features = DiscoverFeatures();
        foreach (var feature in features)
        {
            feature.RegisterFeature(services);
            RegisteredFeatures.Add(feature);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in RegisteredFeatures)
        {
            module.MapEndpoints(app);
        }
        return app;
    }
    
    private static IEnumerable<IFeature> DiscoverFeatures()
    {
        return typeof(IFeature).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IFeature)))
            .Select(Activator.CreateInstance)
            .Cast<IFeature>();
    }
}