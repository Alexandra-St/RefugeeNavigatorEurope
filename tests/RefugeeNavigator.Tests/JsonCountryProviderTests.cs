using System.Text.Json;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class JsonCountryProviderTests
{
    [Fact]
    public async Task LoadAsyncReturnsCountries()
    {
        var expected = new[]
        {
            new Country(Guid.Parse("11111111-1111-1111-1111-111111111111"), "First Country", "first-country"),
            new Country(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Second Country", "second-country")
        };
        var json = JsonSerializer.Serialize(expected);

        await WithTemporaryFileAsync(json, async filePath =>
        {
            var provider = new JsonCountryProvider(filePath);

            var countries = await provider.LoadAsync();

            Assert.Equal(2, countries.Count);
            for (var index = 0; index < expected.Length; index++)
            {
                Assert.Equal(expected[index].Id, countries[index].Id);
                Assert.Equal(expected[index].Name, countries[index].Name);
                Assert.Equal(expected[index].Slug, countries[index].Slug);
            }
        });
    }

    [Fact]
    public async Task LoadAsyncReturnsEmptyCollectionForEmptyArray()
    {
        await WithTemporaryFileAsync("[]", async filePath =>
        {
            var provider = new JsonCountryProvider(filePath);

            var countries = await provider.LoadAsync();

            Assert.Empty(countries);
        });
    }

    [Fact]
    public async Task LoadAsyncThrowsForMissingFile()
    {
        var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.json");
        var provider = new JsonCountryProvider(filePath);

        await Assert.ThrowsAsync<FileNotFoundException>(() => provider.LoadAsync());
    }

    [Fact]
    public async Task LoadAsyncThrowsForInvalidJson()
    {
        await WithTemporaryFileAsync("[{ invalid json ]", async filePath =>
        {
            var provider = new JsonCountryProvider(filePath);

            await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync());
        });
    }

    [Fact]
    public async Task LoadAsyncThrowsForJsonNull()
    {
        await WithTemporaryFileAsync("null", async filePath =>
        {
            var provider = new JsonCountryProvider(filePath);

            var exception = await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync());

            Assert.Equal("The country data must be a JSON array.", exception.Message);
        });
    }

    [Fact]
    public async Task LoadAsyncSupportsCancellation()
    {
        await WithTemporaryFileAsync("[]", async filePath =>
        {
            var provider = new JsonCountryProvider(filePath);
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => provider.LoadAsync(cancellationTokenSource.Token));
        });
    }

    private static async Task WithTemporaryFileAsync(string contents, Func<string, Task> action)
    {
        var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.json");

        try
        {
            await File.WriteAllTextAsync(filePath, contents);
            await action(filePath);
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}
