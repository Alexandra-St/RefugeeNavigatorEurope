using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class JsonOfficialSourceProvider
{
    private readonly string _filePath;

    public JsonOfficialSourceProvider(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<IReadOnlyList<OfficialSource>> LoadAsync(CancellationToken cancellationToken = default)
    {
        var sources = await JsonArrayFileReader.ReadAsync<OfficialSource?>(
            _filePath,
            "official source data",
            cancellationToken);

        if (sources.Any(source => source is null))
        {
            throw new InvalidDataException("The official source data must not contain null sources.");
        }

        return sources.Select(source => source!).ToList().AsReadOnly();
    }
}
