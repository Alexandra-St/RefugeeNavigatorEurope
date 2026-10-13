using System.Text.Json;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class JsonInformationCategoryProvider
{
    private readonly string _filePath;

    public JsonInformationCategoryProvider(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<IReadOnlyList<InformationCategory>> LoadAsync(
        CancellationToken cancellationToken = default)
    {
        await using var stream = new FileStream(
            _filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        var categories = await JsonSerializer.DeserializeAsync<List<InformationCategory>>(
            stream,
            cancellationToken: cancellationToken);

        return categories?.AsReadOnly()
            ?? throw new JsonException("The information category data must be a JSON array.");
    }
}
