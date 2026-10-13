using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
var catalog = await new JsonCatalogProvider(builder.Environment.ContentRootPath).LoadAsync();
builder.Services.AddSingleton(catalog);

var supportedCultures = new[]
{
    new CultureInfo("uk"),
    new CultureInfo("en"),
    new CultureInfo("ru")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new IRequestCultureProvider[]
    {
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp => errorApp.Run(context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Task.CompletedTask;
    }));
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStatusCodePagesWithReExecute("/not-found");
app.UseAntiforgery();
app.MapGet("/set-language", (HttpContext context, string? culture, string? returnUrl) =>
{
    if (culture is not ("uk" or "en" or "ru"))
    {
        return Results.BadRequest();
    }

    var actionContext = new ActionContext(context, context.GetRouteData() ?? new RouteData(), new ActionDescriptor());
    var urlHelper = new UrlHelper(actionContext);
    var redirectUrl = urlHelper.IsLocalUrl(returnUrl) ? returnUrl! : "/";

    context.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions
        {
            HttpOnly = true,
            Secure = context.Request.IsHttps,
            SameSite = SameSiteMode.Lax,
            IsEssential = true,
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            Path = "/"
        });

    return Results.LocalRedirect(redirectUrl);
});
app.MapStaticAssets();
app.MapRazorComponents<App>();
app.Run();
