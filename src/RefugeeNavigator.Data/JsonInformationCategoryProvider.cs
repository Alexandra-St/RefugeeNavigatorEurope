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
        var categories = await JsonArrayFileReader.ReadAsync<InformationCategory>(
            _filePath,
            "information category data",
            cancellationToken);

        return categories.AsReadOnly();
    }
}
