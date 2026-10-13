using System.Globalization;
using System.Resources;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public class CatalogLocalizationTests
{
    [Theory]
    [InlineData("denmark", "Country.Denmark")]
    [InlineData("germany", "Country.Germany")]
    [InlineData("poland", "Country.Poland")]
    public void CountrySlugResolvesToExistingResourceKey(string slug, string expectedKey)
    {
        var country = new Country(Guid.NewGuid(), "Display name", slug);

        Assert.Equal(expectedKey, CatalogLocalizationKeyResolver.CountryName(country));
    }

    [Theory]
    [InlineData("temporary-protection", "Category.TemporaryProtection")]
    [InlineData("employment", "Category.Employment")]
    [InlineData("healthcare", "Category.Healthcare")]
    [InlineData("housing", "Category.Housing")]
    [InlineData("education", "Category.Education")]
    [InlineData("social-assistance", "Category.SocialAssistance")]
    public void CategorySlugResolvesToExistingResourceKey(string slug, string expectedKey)
    {
        var category = new InformationCategory(Guid.NewGuid(), "Display name", slug);

        Assert.Equal(expectedKey, CatalogLocalizationKeyResolver.CategoryName(category));
    }

    [Theory]
    [InlineData("temporary-protection", "Category.TemporaryProtectionDescription")]
    [InlineData("employment", "Category.EmploymentDescription")]
    [InlineData("healthcare", "Category.HealthcareDescription")]
    [InlineData("housing", "Category.HousingDescription")]
    [InlineData("education", "Category.EducationDescription")]
    [InlineData("social-assistance", "Category.SocialAssistanceDescription")]
    public void CategorySlugResolvesToExistingDescriptionKey(string slug, string expectedKey)
    {
        var category = new InformationCategory(Guid.NewGuid(), "Display name", slug);

        Assert.Equal(expectedKey, CatalogLocalizationKeyResolver.CategoryDescription(category));
    }

    [Fact]
    public async Task CheckedInCatalogHasNonemptyTranslationsInEverySupportedCulture()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var resources = CreateResourceManager();

        CatalogLocalizationValidator.Validate(catalog, resources.GetString);

        foreach (var cultureName in new[] { "uk", "en", "ru" })
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            Assert.All(catalog.Countries, country =>
                Assert.False(string.IsNullOrWhiteSpace(resources.GetString(
                    CatalogLocalizationKeyResolver.CountryName(country), culture))));
            Assert.All(catalog.Categories, category =>
            {
                Assert.False(string.IsNullOrWhiteSpace(resources.GetString(
                    CatalogLocalizationKeyResolver.CategoryName(category), culture)));
                Assert.False(string.IsNullOrWhiteSpace(resources.GetString(
                    CatalogLocalizationKeyResolver.CategoryDescription(category), culture)));
            });
        }
    }

    [Fact]
    public async Task MissingTranslationFailsValidationWithKeyAndCulture()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        const string missingKey = "Country.Denmark";

        var exception = Assert.Throws<InvalidDataException>(() =>
            CatalogLocalizationValidator.Validate(catalog, (key, culture) =>
                key == missingKey && culture.Name == "ru" ? null : "translated"));

        Assert.Contains(missingKey, exception.Message);
        Assert.Contains("ru", exception.Message);
    }

    [Fact]
    public async Task EmptyTranslationFailsValidationWithKeyAndCulture()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        const string emptyKey = "Category.EmploymentDescription";

        var exception = Assert.Throws<InvalidDataException>(() =>
            CatalogLocalizationValidator.Validate(catalog, (key, culture) =>
                key == emptyKey && culture.Name == "en" ? "  " : "translated"));

        Assert.Contains(emptyKey, exception.Message);
        Assert.Contains("en", exception.Message);
    }

    [Fact]
    public async Task MissingTranslationPreventsSuccessfulStartupValidation()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

        Assert.Throws<InvalidDataException>(() =>
            CatalogLocalizationValidator.Validate(catalog, (_, _) => null));
    }

    private static ResourceManager CreateResourceManager() => new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);
}
