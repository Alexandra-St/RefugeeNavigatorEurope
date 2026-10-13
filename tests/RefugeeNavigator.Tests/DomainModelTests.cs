using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class DomainModelTests
{
    [Fact]
    public void CountryConstructorPreservesPropertyValues()
    {
        var id = Guid.NewGuid();

        var country = new Country(id, "Example Country", "example-country");

        Assert.Equal(id, country.Id);
        Assert.Equal("Example Country", country.Name);
        Assert.Equal("example-country", country.Slug);
    }

    [Fact]
    public void InformationCategoryConstructorPreservesPropertyValues()
    {
        var id = Guid.NewGuid();

        var category = new InformationCategory(id, "Example Category", "example-category");

        Assert.Equal(id, category.Id);
        Assert.Equal("Example Category", category.Name);
        Assert.Equal("example-category", category.Slug);
    }

    [Fact]
    public void InformationEntryConstructorPreservesPropertyValues()
    {
        var id = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        var entry = new InformationEntry(id, countryId, categoryId);

        Assert.Equal(id, entry.Id);
        Assert.Equal(countryId, entry.CountryId);
        Assert.Equal(categoryId, entry.CategoryId);
    }

    [Fact]
    public void InformationEntryPreservesIndependentCountryAndCategoryIdentifiers()
    {
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var countryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var categoryId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var entry = new InformationEntry(id, countryId, categoryId);
        var swappedEntry = new InformationEntry(id, categoryId, countryId);

        Assert.Equal(countryId, entry.CountryId);
        Assert.Equal(categoryId, entry.CategoryId);
        Assert.NotEqual(entry.CountryId, entry.CategoryId);
        Assert.Equal(categoryId, swappedEntry.CountryId);
        Assert.Equal(countryId, swappedEntry.CategoryId);
    }

    [Fact]
    public void InformationEntryPropertiesHaveNoSetters()
    {
        foreach (var propertyName in new[] { "Id", "CountryId", "CategoryId" })
        {
            var property = typeof(InformationEntry).GetProperty(propertyName);

            Assert.NotNull(property);
            Assert.Null(property.GetSetMethod(nonPublic: true));
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public void InformationEntryTranslationConstructorPreservesPropertyValues(string languageCode)
    {
        var entryId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var updatedAt = new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.FromHours(2));

        var translation = new InformationEntryTranslation(
            entryId,
            languageCode,
            "Example Title",
            "Example Summary",
            "Example Content",
            VerificationStatus.Verified,
            updatedAt);

        Assert.Equal(entryId, translation.InformationEntryId);
        Assert.Equal(languageCode, translation.LanguageCode);
        Assert.Equal("Example Title", translation.Title);
        Assert.Equal("Example Summary", translation.Summary);
        Assert.Equal("Example Content", translation.Content);
        Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
        Assert.Equal(updatedAt, translation.LastUpdatedAt);
        Assert.Equal(updatedAt.Offset, translation.LastUpdatedAt.Offset);
    }

    [Fact]
    public void InformationEntryTranslationPropertiesHaveNoSetters()
    {
        foreach (var propertyName in new[]
        {
            "InformationEntryId", "LanguageCode", "Title", "Summary", "Content", "VerificationStatus", "LastUpdatedAt"
        })
        {
            var property = typeof(InformationEntryTranslation).GetProperty(propertyName);

            Assert.NotNull(property);
            Assert.Null(property.GetSetMethod(nonPublic: true));
        }
    }

    [Fact]
    public void OfficialSourceConstructorPreservesPropertyValues()
    {
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var informationEntryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var url = new Uri("https://example.org/source");
        var lastCheckedAt = new DateTimeOffset(2026, 10, 12, 9, 30, 0, TimeSpan.Zero);

        var source = new OfficialSource(id, informationEntryId, "Example Source", url, lastCheckedAt);

        Assert.Equal(id, source.Id);
        Assert.Equal(informationEntryId, source.InformationEntryId);
        Assert.Equal("Example Source", source.Title);
        Assert.Equal(url, source.Url);
        Assert.Equal(lastCheckedAt, source.LastCheckedAt);
    }

    [Fact]
    public void OfficialSourcePropertiesHaveNoSetters()
    {
        foreach (var propertyName in new[] { "Id", "InformationEntryId", "Title", "Url", "LastCheckedAt" })
        {
            var property = typeof(OfficialSource).GetProperty(propertyName);

            Assert.NotNull(property);
            Assert.Null(property.GetSetMethod(nonPublic: true));
        }
    }

    [Fact]
    public void OfficialSourcePreservesTimestampOffset()
    {
        var lastCheckedAt = new DateTimeOffset(2026, 10, 12, 14, 45, 0, TimeSpan.FromHours(5.5));
        var source = new OfficialSource(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Example Source",
            new Uri("https://example.org/source"),
            lastCheckedAt);

        Assert.Equal(lastCheckedAt, source.LastCheckedAt);
        Assert.Equal(lastCheckedAt.Offset, source.LastCheckedAt.Offset);
    }

    [Theory]
    [InlineData(typeof(Country))]
    [InlineData(typeof(InformationCategory))]
    public void ModelPropertiesHaveNoSetters(Type modelType)
    {
        foreach (var propertyName in new[] { "Id", "Name", "Slug" })
        {
            var property = modelType.GetProperty(propertyName);

            Assert.NotNull(property);
            Assert.Null(property.GetSetMethod(nonPublic: true));
        }
    }
}
