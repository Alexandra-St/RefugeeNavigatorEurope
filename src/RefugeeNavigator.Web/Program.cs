using System.Globalization;
using Microsoft.AspNetCore.Localization;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

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
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>();
app.Run();
