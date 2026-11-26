using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Web.Seo;

public sealed class SeoTranslationAvailability(CatalogData catalog, StepByStepGuideCatalog guides)
{
    public IReadOnlyList<(string Language, string Path)> StaticPage(string path) =>
        SupportedCultures.All.Select(c => (c.TwoLetterISOLanguageName, LocalizedUrl.ForCurrentCulture(path, c.TwoLetterISOLanguageName))).ToArray();

    public IReadOnlyList<(string Language, string Path)> Country(string slug) =>
        SupportedCultures.All.Where(c => HasCountryTranslation(slug, c.TwoLetterISOLanguageName))
            .Select(c => (c.TwoLetterISOLanguageName, LocalizedUrl.ForCurrentCulture($"/countries/{slug}", c.TwoLetterISOLanguageName))).ToArray();

    public IReadOnlyList<(string Language, string Path)> Category(string countrySlug, string categorySlug) =>
        SupportedCultures.All.Where(c => HasCategoryTranslation(countrySlug, categorySlug, c.TwoLetterISOLanguageName))
            .Select(c => (c.TwoLetterISOLanguageName, LocalizedUrl.ForCurrentCulture($"/countries/{countrySlug}/categories/{categorySlug}", c.TwoLetterISOLanguageName))).ToArray();

    public IReadOnlyList<(string Language, string Path)> Article(Guid articleId)
    {
        var entry = catalog.InformationEntries.FirstOrDefault(item => item.Id == articleId);
        if (entry is null) return [];
        var resolver = new ArticleUrlResolver(catalog);
        return SupportedCultures.All.Select(culture => culture.TwoLetterISOLanguageName)
            .Select(language => (Language: language, Path: resolver.Generate(entry, language)))
            .Where(item => item.Path is not null)
            .Select(item => (item.Language, item.Path!)).ToArray();
    }

    public IReadOnlyList<(string Language, string Path)> Guide(string countrySlug, string guideSlug)
    {
        var guide = StepByStepGuideResolver.Resolve(guides, catalog, countrySlug, guideSlug);
        if (guide is null || !StepByStepGuideVisibility.CanRender(guide.EditorialStatus, false)) return [];
        return SupportedCultures.All.Where(c => guide.Translations.TryGetValue(c.TwoLetterISOLanguageName, out var t)
                && !string.IsNullOrWhiteSpace(t.Title) && !string.IsNullOrWhiteSpace(t.Description))
            .Select(c => (c.TwoLetterISOLanguageName, LocalizedUrl.ForCurrentCulture($"/guides/{countrySlug}/{guideSlug}", c.TwoLetterISOLanguageName))).ToArray();
    }

    private bool HasCountryTranslation(string slug, string language)
    {
        var country = catalog.Countries.FirstOrDefault(c => c.Slug == slug);
        return country is not null && CountryVisibility.GetVisibleCountries(catalog, false).Contains(country)
            && catalog.InformationEntries.Where(e => e.CountryId == country.Id).Any(e => IsPublishedTranslation(e.Id, language));
    }

    private bool HasCategoryTranslation(string countrySlug, string categorySlug, string language)
    {
        var country = catalog.Countries.FirstOrDefault(c => c.Slug == countrySlug);
        var category = catalog.Categories.FirstOrDefault(c => c.Slug == categorySlug);
        return country is not null && category is not null
            && CountryVisibility.GetVisibleCountries(catalog, false).Contains(country)
            && catalog.InformationEntries.Where(e => e.CountryId == country.Id && e.CategoryId == category.Id)
                .Any(e => IsPublishedTranslation(e.Id, language));
    }

    private bool IsPublishedTranslation(Guid entryId, string language) => catalog.InformationEntryTranslations.Any(t =>
        t.InformationEntryId == entryId && t.LanguageCode == language
        && t.VerificationStatus == VerificationStatus.Verified);

}
