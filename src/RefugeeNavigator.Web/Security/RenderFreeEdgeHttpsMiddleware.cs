using Microsoft.AspNetCore.Http;

namespace RefugeeNavigator.Web.Security;

public sealed class RenderFreeEdgeHttpsMiddleware(
    RequestDelegate next,
    PrivateAlphaAccessOptions options)
{
    public Task InvokeAsync(HttpContext context)
    {
        if (options.UsesRenderFreeEdgeTls)
        {
            // Render Free web services only receive public traffic through the HTTPS-terminating edge.
            // Do not derive this from client-controlled forwarded headers.
            context.Request.Scheme = Uri.UriSchemeHttps;
        }

        return next(context);
    }
}
