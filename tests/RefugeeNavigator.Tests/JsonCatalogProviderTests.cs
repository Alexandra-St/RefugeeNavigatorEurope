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
            });
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

    private static async Task WithCatalogFilesAsync(
        string? countriesJson,
        string categoriesJson,
        Func<JsonCatalogProvider, Task> action)
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
            await action(new JsonCatalogProvider(contentRootPath));
        }
        finally
        {
            Directory.Delete(contentRootPath, recursive: true);
        }
    }
}
