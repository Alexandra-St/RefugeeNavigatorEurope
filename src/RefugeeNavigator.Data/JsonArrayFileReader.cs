using System.Text.Json;

namespace RefugeeNavigator.Data;

internal static class JsonArrayFileReader
{
    public static async Task<List<T>> ReadAsync<T>(
        string filePath,
        string dataDescription,
        CancellationToken cancellationToken)
    {
        await using var stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        return await JsonSerializer.DeserializeAsync<List<T>>(
                   stream,
                   cancellationToken: cancellationToken)
               ?? throw new JsonException($"The {dataDescription} must be a JSON array.");
    }
}
