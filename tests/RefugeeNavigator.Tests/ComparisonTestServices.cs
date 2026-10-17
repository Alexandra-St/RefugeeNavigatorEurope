using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RefugeeNavigator.Data;
using RefugeeNavigator.Web.Comparison;

namespace RefugeeNavigator.Tests;

internal static class ComparisonTestServices
{
    private static readonly Lazy<Task<(CatalogData Catalog, ComparisonFactCatalog Facts)>> Data = new(async () =>
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var facts = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        return (catalog, facts);
    });

    public static async Task RegisterAsync(IServiceCollection services)
    {
        var (_, facts) = await Data.Value;
        services.AddSingleton(facts);
        services.AddSingleton<IWebHostEnvironment>(new DevelopmentEnvironment());
    }

    private sealed class DevelopmentEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = "Development";
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
