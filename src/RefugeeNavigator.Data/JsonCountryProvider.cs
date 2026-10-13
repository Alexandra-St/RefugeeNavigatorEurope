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
        var countries = await JsonArrayFileReader.ReadAsync<Country>(
            _filePath,
            "country data",
            cancellationToken);

        return countries.AsReadOnly();
    }
}
