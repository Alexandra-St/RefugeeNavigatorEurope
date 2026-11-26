using Microsoft.AspNetCore.Localization;

namespace RefugeeNavigator.Web.Localization;

public sealed class CircuitRequestCultureProvider : RequestCultureProvider
{
    public const string QueryKey = "routeCulture";

    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var path = request.Path.Value?.TrimEnd('/');
        if (path is not ("/_blazor" or "/_blazor/negotiate"))
        {
            return NullProviderCultureResult;
        }

        var values = request.Query[QueryKey];
        if (values.Count != 1 || !SupportedCultures.All.Any(culture => culture.Name == values[0]))
        {
            return NullProviderCultureResult;
        }

        return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(values[0]!));
    }
}
