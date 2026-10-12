using System.Text.Json;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class JsonCountryProvider
{
    private readonly string _filePath;

    public JsonCountryProvider(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<IReadOnlyList<Country>> LoadAsync(CancellationToken cancellationToken = default)
    {
        await using var stream = new FileStream(
            _filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        var countries = await JsonSerializer.DeserializeAsync<List<Country>>(
            stream,
            cancellationToken: cancellationToken);

        return countries?.AsReadOnly()
            ?? throw new JsonException("The country data must be a JSON array.");
    }
}
