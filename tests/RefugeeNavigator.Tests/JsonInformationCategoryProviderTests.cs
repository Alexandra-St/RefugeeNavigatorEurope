using System.Text.Json;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class JsonInformationCategoryProviderTests
{
    [Fact]
    public async Task LoadAsyncReturnsInformationCategories()
    {
        var expected = new[]
        {
            new InformationCategory(
                Guid.Parse("20000000-0000-0000-0000-000000000001"), "Employment", "employment"),
            new InformationCategory(
                Guid.Parse("20000000-0000-0000-0000-000000000002"), "Housing", "housing")
        };

        await WithTemporaryFileAsync(JsonSerializer.Serialize(expected), async filePath =>
        {
            var provider = new JsonInformationCategoryProvider(filePath);

            var categories = await provider.LoadAsync();

            Assert.Equal(
                expected.Select(category => (category.Id, category.Name, category.Slug)),
                categories.Select(category => (category.Id, category.Name, category.Slug)));
        });
    }

    [Fact]
    public async Task LoadAsyncReturnsEmptyCollectionForEmptyArray()
    {
        await WithTemporaryFileAsync("[]", async filePath =>
        {
            var provider = new JsonInformationCategoryProvider(filePath);

            var categories = await provider.LoadAsync();

            Assert.Empty(categories);
        });
    }

    [Fact]
    public async Task LoadAsyncThrowsForMissingFile()
    {
        var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.json");
        var provider = new JsonInformationCategoryProvider(filePath);

        await Assert.ThrowsAsync<FileNotFoundException>(() => provider.LoadAsync());
    }

    [Fact]
    public async Task LoadAsyncThrowsForInvalidJson()
    {
        await WithTemporaryFileAsync("[{ invalid json ]", async filePath =>
        {
            var provider = new JsonInformationCategoryProvider(filePath);

            await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync());
        });
    }

    [Fact]
    public async Task LoadAsyncThrowsForJsonNull()
    {
        await WithTemporaryFileAsync("null", async filePath =>
        {
            var provider = new JsonInformationCategoryProvider(filePath);

            var exception = await Assert.ThrowsAsync<JsonException>(() => provider.LoadAsync());

            Assert.Equal("The information category data must be a JSON array.", exception.Message);
        });
    }

    [Fact]
    public async Task LoadAsyncSupportsCancellation()
    {
        await WithTemporaryFileAsync("[]", async filePath =>
        {
            var provider = new JsonInformationCategoryProvider(filePath);
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
