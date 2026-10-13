using System.Text.Json;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class JsonCatalogProviderTests
{
    private static readonly Guid CountryId1 = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid CountryId2 = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid CategoryId1 = Guid.Parse("20000000-0000-0000-0000-000000000001");
    private static readonly Guid CategoryId2 = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid EntryId1 = Guid.Parse("30000000-0000-0000-0000-000000000001");
    private static readonly Guid EntryId2 = Guid.Parse("30000000-0000-0000-0000-000000000002");
    private static readonly Guid SourceId1 = Guid.Parse("40000000-0000-0000-0000-000000000001");
    private static readonly Guid SourceId2 = Guid.Parse("40000000-0000-0000-0000-000000000002");

    [Fact]
    public async Task LoadAsyncLoadsCheckedInCatalogWithStableIdentifiersAndSlugs()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

        Assert.Equal(
            new[]
            {
                (Guid.Parse("10000000-0000-0000-0000-000000000001"), "Denmark", "denmark"),
                (Guid.Parse("10000000-0000-0000-0000-000000000002"), "Germany", "germany"),
                (Guid.Parse("10000000-0000-0000-0000-000000000003"), "Poland", "poland")
            },
            catalog.Countries.Select(country => (country.Id, country.Name, country.Slug)));
        Assert.Equal(
            new[]
            {
                (Guid.Parse("20000000-0000-0000-0000-000000000001"), "Temporary protection", "temporary-protection"),
                (Guid.Parse("20000000-0000-0000-0000-000000000002"), "Employment", "employment"),
                (Guid.Parse("20000000-0000-0000-0000-000000000003"), "Healthcare", "healthcare"),
                (Guid.Parse("20000000-0000-0000-0000-000000000004"), "Housing", "housing"),
                (Guid.Parse("20000000-0000-0000-0000-000000000005"), "Education", "education"),
                (Guid.Parse("20000000-0000-0000-0000-000000000006"), "Social assistance", "social-assistance")
            },
            catalog.Categories.Select(category => (category.Id, category.Name, category.Slug)));
        Assert.Empty(catalog.InformationEntries);
        Assert.Empty(catalog.InformationEntryTranslations);
        Assert.Empty(catalog.OfficialSources);
    }

    [Fact]
    public async Task LoadAsyncReturnsValidatedCatalogWithStableIdentifiersAndSlugs()
    {
        var countries = new[]
        {
            new Country(CountryId1, "Denmark", "denmark"),
            new Country(CountryId2, "Germany", "germany")
        };
        var categories = new[]
        {
            new InformationCategory(CategoryId1, "Temporary protection", "temporary-protection"),
            new InformationCategory(CategoryId2, "Employment", "employment")
        };

        await WithCatalogFilesAsync(
            JsonSerializer.Serialize(countries),
            JsonSerializer.Serialize(categories),
            async provider =>
            {
                var catalog = await provider.LoadAsync();

                Assert.Equal(
                    countries.Select(country => (country.Id, country.Name, country.Slug)),
                    catalog.Countries.Select(country => (country.Id, country.Name, country.Slug)));
                Assert.Equal(
                    categories.Select(category => (category.Id, category.Name, category.Slug)),
                    catalog.Categories.Select(category => (category.Id, category.Name, category.Slug)));
                Assert.IsAssignableFrom<IReadOnlyList<Country>>(catalog.Countries);
                Assert.IsAssignableFrom<IReadOnlyList<InformationCategory>>(catalog.Categories);
                Assert.Empty(catalog.InformationEntries);
                Assert.Empty(catalog.InformationEntryTranslations);
                Assert.Empty(catalog.OfficialSources);
            });
    }

    [Fact]
    public async Task LoadAsyncLoadsMultilingualEntriesAndMultipleSourcesIntoReadOnlyCollections()
    {
        var translations = new[]
        {
            Translation("uk"),
            Translation("en"),
            Translation("ru")
        };
        var sources = new[]
        {
            Source(SourceId1, "https://example.gov/first"),
            Source(SourceId2, "https://example.gov/second")
        };

        await WithCatalogFilesAsync(
            ValidCountriesJson(),
            ValidCategoriesJson(),
            async provider =>
            {
                var catalog = await provider.LoadAsync();

                Assert.Equal(new[] { EntryId1 }, catalog.InformationEntries.Select(entry => entry.Id));
                Assert.Equal(new[] { "uk", "en", "ru" }, catalog.InformationEntryTranslations.Select(item => item.LanguageCode));
                Assert.All(catalog.InformationEntryTranslations, item => Assert.Equal(EntryId1, item.InformationEntryId));
                Assert.Equal(sources.Select(source => source.Id), catalog.OfficialSources.Select(source => source.Id));
                Assert.All(catalog.OfficialSources, source => Assert.Equal(EntryId1, source.InformationEntryId));
                Assert.True(Assert.IsAssignableFrom<IList<InformationEntry>>(catalog.InformationEntries).IsReadOnly);
                Assert.True(Assert.IsAssignableFrom<IList<InformationEntryTranslation>>(catalog.InformationEntryTranslations).IsReadOnly);
                Assert.True(Assert.IsAssignableFrom<IList<OfficialSource>>(catalog.OfficialSources).IsReadOnly);
            },
            EntriesJson(EntryId1, translations),
            JsonSerializer.Serialize(sources));
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenInformationEntryJsonIsMalformed()
    {
        await WithCatalogFilesAsync(ValidCountriesJson(), ValidCategoriesJson(), async provider =>
            await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync()), "[{ invalid", "[]");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenOfficialSourceJsonIsMalformed()
    {
        await WithCatalogFilesAsync(ValidCountriesJson(), ValidCategoriesJson(), async provider =>
            await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync()), "[]", "[{ invalid");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenInformationEntryFileIsMissing()
    {
        await WithCatalogFilesAsync(ValidCountriesJson(), ValidCategoriesJson(), async provider =>
            await Assert.ThrowsAsync<FileNotFoundException>(() => provider.LoadAsync()), null, "[]");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenOfficialSourceFileIsMissing()
    {
        await WithCatalogFilesAsync(ValidCountriesJson(), ValidCategoriesJson(), async provider =>
            await Assert.ThrowsAsync<FileNotFoundException>(() => provider.LoadAsync()), "[]", null);
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCancellationIsRequested()
    {
        await WithCatalogFilesAsync(ValidCountriesJson(), ValidCategoriesJson(), async provider =>
            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => provider.LoadAsync(new CancellationToken(canceled: true))));
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenInformationEntryHasNoTranslations()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Array.Empty<InformationEntryTranslation>()), "at least one translation");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenInformationEntryIdIsDuplicated()
    {
        var entries = JsonSerializer.Serialize(new[]
        {
            EntryDocument(EntryId1, Translations(EntryId1)),
            EntryDocument(EntryId1, Translations(EntryId1))
        });

        await AssertInvalidContentAsync(entries, "InformationEntry: duplicate Id.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenTranslationLanguageIsDuplicated()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, new[] { Translation("uk"), Translation("uk"), Translation("ru") }),
            "InformationEntryTranslation: duplicate InformationEntryId and LanguageCode.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenRequiredTranslationIsMissing()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, new[] { Translation("uk"), Translation("en") }), "missing translation for language 'ru'");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenTranslationLanguageIsUnsupported()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, new[] { Translation("uk"), Translation("en"), Translation("de") }), "LanguageCode must be one of the supported values: uk, en, ru.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenEntryCountryReferenceIsMissing()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId1), Guid.NewGuid()), "InformationEntry: CountryId references a missing Country.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenEntryCategoryReferenceIsMissing()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId1), categoryId: Guid.NewGuid()), "InformationEntry: CategoryId references a missing InformationCategory.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenTranslationReferencesDifferentEntry()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId2)), "InformationEntryTranslation: InformationEntryId references a missing InformationEntry.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenOfficialSourceReferencesMissingEntry()
    {
        await AssertInvalidContentAsync("[]", JsonSerializer.Serialize(new[] { Source(SourceId1, "https://example.gov", EntryId2) }), "OfficialSource: InformationEntryId references a missing InformationEntry.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenOfficialSourceIdIsDuplicated()
    {
        var sources = JsonSerializer.Serialize(new[]
        {
            Source(SourceId1, "https://example.gov/first"),
            Source(SourceId1, "https://example.gov/second")
        });

        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId1)), sources, "OfficialSource: duplicate Id.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenOfficialSourceUrlIsInvalid()
    {
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId1)),
            JsonSerializer.Serialize(new[] { Source(SourceId1, "file:///tmp/source") }), "Url must use HTTP or HTTPS.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenContentTimestampIsInvalidOrInFuture()
    {
        var futureTranslation = new InformationEntryTranslation(EntryId1, "uk", "Title", "Summary", "Content", VerificationStatus.NeedsReview,
            DateTimeOffset.MaxValue);
        await AssertInvalidContentAsync(EntriesJson(EntryId1, new[] { futureTranslation, Translation("en"), Translation("ru") }), "future");

        var defaultTimestampTranslation = new InformationEntryTranslation(EntryId1, "uk", "Title", "Summary", "Content",
            VerificationStatus.NeedsReview, default);
        await AssertInvalidContentAsync(EntriesJson(EntryId1, new[] { defaultTimestampTranslation, Translation("en"), Translation("ru") }), "LastUpdatedAt must not be default.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenOfficialSourceTimestampIsInvalidOrInFuture()
    {
        var source = new OfficialSource(SourceId1, EntryId1, "Official source", new Uri("https://example.gov"), DateTimeOffset.MaxValue);
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId1)), JsonSerializer.Serialize(new[] { source }), "future");

        var defaultTimestampSource = new OfficialSource(SourceId1, EntryId1, "Official source", new Uri("https://example.gov"), default);
        await AssertInvalidContentAsync(EntriesJson(EntryId1, Translations(EntryId1)), JsonSerializer.Serialize(new[] { defaultTimestampSource }), "LastCheckedAt must not be default.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCountryCatalogIsEmpty()
    {
        await AssertInvalidCatalogAsync("[]", ValidCategoriesJson(), "at least one country");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCategoryCatalogIsEmpty()
    {
        await AssertInvalidCatalogAsync(ValidCountriesJson(), "[]", "at least one information category");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCountryModelIsInvalid()
    {
        var invalidCountry = JsonSerializer.Serialize(
            new[] { new Country(Guid.Empty, " ", "Denmark") });

        await AssertInvalidCatalogAsync(invalidCountry, ValidCategoriesJson(), "Country catalog is invalid");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCategoryModelIsInvalid()
    {
        var invalidCategory = JsonSerializer.Serialize(
            new[] { new InformationCategory(Guid.Empty, " ", "Employment") });

        await AssertInvalidCatalogAsync(ValidCountriesJson(), invalidCategory, "Information category catalog is invalid");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCountryIdsAreDuplicated()
    {
        var countries = JsonSerializer.Serialize(new[]
        {
            new Country(CountryId1, "Denmark", "denmark"),
            new Country(CountryId1, "Germany", "germany")
        });

        await AssertInvalidCatalogAsync(countries, ValidCategoriesJson(), "Country: duplicate Id.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCountrySlugsAreDuplicated()
    {
        var countries = JsonSerializer.Serialize(new[]
        {
            new Country(CountryId1, "Denmark", "denmark"),
            new Country(CountryId2, "Another Denmark", "denmark")
        });

        await AssertInvalidCatalogAsync(countries, ValidCategoriesJson(), "Country: duplicate Slug.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCategoryIdsAreDuplicated()
    {
        var categories = JsonSerializer.Serialize(new[]
        {
            new InformationCategory(CategoryId1, "Employment", "employment"),
            new InformationCategory(CategoryId1, "Housing", "housing")
        });

        await AssertInvalidCatalogAsync(ValidCountriesJson(), categories, "InformationCategory: duplicate Id.");
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCategorySlugsAreDuplicated()
    {
        var categories = JsonSerializer.Serialize(new[]
        {
            new InformationCategory(CategoryId1, "Employment", "employment"),
            new InformationCategory(CategoryId2, "Other employment", "employment")
        });

        await AssertInvalidCatalogAsync(ValidCountriesJson(), categories, "InformationCategory: duplicate Slug.");
    }

    [Fact]
    public async Task LoadAsyncThrowsForMalformedJson()
    {
        await WithCatalogFilesAsync("[{ invalid json ]", ValidCategoriesJson(), async provider =>
        {
            await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync());
        });
    }

    [Fact]
    public async Task LoadAsyncThrowsWhenCatalogFileIsMissing()
    {
        await WithCatalogFilesAsync(null, ValidCategoriesJson(), async provider =>
        {
            await Assert.ThrowsAsync<FileNotFoundException>(() => provider.LoadAsync());
        });
    }

    [Fact]
    public async Task InvalidCatalogPreventsCatalogLoadingBeforeStartupRegistration()
    {
        await WithCatalogFilesAsync("[]", ValidCategoriesJson(), async provider =>
        {
            var exception = await Assert.ThrowsAsync<InvalidDataException>(() => provider.LoadAsync());

            Assert.Contains("Country catalog is invalid", exception.Message);
        });
    }

    private static async Task AssertInvalidCatalogAsync(
        string countriesJson,
        string categoriesJson,
        string expectedMessage)
    {
        await WithCatalogFilesAsync(countriesJson, categoriesJson, async provider =>
        {
            var exception = await Assert.ThrowsAsync<InvalidDataException>(() => provider.LoadAsync());

            Assert.Contains(expectedMessage, exception.Message);
        });
    }

    private static string ValidCountriesJson() => JsonSerializer.Serialize(
        new[] { new Country(CountryId1, "Denmark", "denmark") });

    private static string ValidCategoriesJson() => JsonSerializer.Serialize(
        new[] { new InformationCategory(CategoryId1, "Employment", "employment") });

    private static InformationEntryTranslation Translation(string languageCode) =>
        new(EntryId1, languageCode, $"Title {languageCode}", $"Summary {languageCode}", $"Content {languageCode}",
            VerificationStatus.NeedsReview, new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero));

    private static InformationEntryTranslation[] Translations(Guid entryId) => new[]
    {
        new InformationEntryTranslation(entryId, "uk", "Title uk", "Summary uk", "Content uk", VerificationStatus.NeedsReview,
            new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero)),
        new InformationEntryTranslation(entryId, "en", "Title en", "Summary en", "Content en", VerificationStatus.NeedsReview,
            new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero)),
        new InformationEntryTranslation(entryId, "ru", "Title ru", "Summary ru", "Content ru", VerificationStatus.NeedsReview,
            new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero))
    };

    private static OfficialSource Source(Guid id, string url, Guid entryId = default) =>
        new(id, entryId == default ? EntryId1 : entryId, "Test source", new Uri(url),
            new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero));

    private static object EntryDocument(Guid entryId, InformationEntryTranslation[] translations) => new
    {
        Id = entryId,
        CountryId = CountryId1,
        CategoryId = CategoryId1,
        Translations = translations
    };

    private static string EntriesJson(
        Guid entryId,
        InformationEntryTranslation[] translations,
        Guid countryId = default,
        Guid categoryId = default) => JsonSerializer.Serialize(new[]
        {
            new
            {
                Id = entryId,
                CountryId = countryId == default ? CountryId1 : countryId,
                CategoryId = categoryId == default ? CategoryId1 : categoryId,
                Translations = translations
            }
        });

    private static async Task AssertInvalidContentAsync(string entriesJson, string expectedMessage) =>
        await AssertInvalidContentAsync(entriesJson, "[]", expectedMessage);

    private static async Task AssertInvalidContentAsync(string entriesJson, string sourcesJson, string expectedMessage)
    {
        await WithCatalogFilesAsync(ValidCountriesJson(), ValidCategoriesJson(), async provider =>
        {
            var exception = await Assert.ThrowsAsync<InvalidDataException>(() => provider.LoadAsync());
            Assert.Contains(expectedMessage, exception.Message, StringComparison.OrdinalIgnoreCase);
        }, entriesJson, sourcesJson);
    }

    private static async Task WithCatalogFilesAsync(
        string? countriesJson,
        string categoriesJson,
        Func<JsonCatalogProvider, Task> action,
        string? informationEntriesJson = "[]",
        string? officialSourcesJson = "[]")
    {
        var contentRootPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var dataDirectory = Path.Combine(contentRootPath, "Data");
        Directory.CreateDirectory(dataDirectory);

        try
        {
            if (countriesJson is not null)
            {
                await File.WriteAllTextAsync(Path.Combine(dataDirectory, "countries.json"), countriesJson);
            }

            await File.WriteAllTextAsync(Path.Combine(dataDirectory, "categories.json"), categoriesJson);
            if (informationEntriesJson is not null)
            {
                await File.WriteAllTextAsync(Path.Combine(dataDirectory, "information-entries.json"), informationEntriesJson);
            }

            if (officialSourcesJson is not null)
            {
                await File.WriteAllTextAsync(Path.Combine(dataDirectory, "official-sources.json"), officialSourcesJson);
            }

            await action(new JsonCatalogProvider(contentRootPath));
        }
        finally
        {
            Directory.Delete(contentRootPath, recursive: true);
        }
    }
}
