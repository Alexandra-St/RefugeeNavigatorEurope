using System.Text.Json;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class JsonInformationEntryProvider
{
    private readonly string _filePath;

    public JsonInformationEntryProvider(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<(IReadOnlyList<InformationEntry> Entries, IReadOnlyList<InformationEntryTranslation> Translations)> LoadAsync(
        CancellationToken cancellationToken = default)
    {
        var documents = await JsonArrayFileReader.ReadAsync<InformationEntryDocument?>(
            _filePath,
            "information entry data",
            cancellationToken);
        var entries = new List<InformationEntry>(documents.Count);
        var translations = new List<InformationEntryTranslation>();

        foreach (var document in documents)
        {
            if (document is null)
            {
                throw new InvalidDataException("The information entry data must not contain null entries.");
            }

            entries.Add(new InformationEntry(document.Id, document.CountryId, document.CategoryId));

            if (document.Translations is null)
            {
                throw new InvalidDataException(
                    $"Information entry '{document.Id}' must contain a translations array.");
            }

            foreach (var translation in document.Translations)
            {
                if (translation is null)
                {
                    throw new InvalidDataException(
                        $"Information entry '{document.Id}' must not contain null translations.");
                }

                translations.Add(translation);
            }
        }

        return (entries.AsReadOnly(), translations.AsReadOnly());
    }

    private sealed record InformationEntryDocument(
        Guid Id,
        Guid CountryId,
        Guid CategoryId,
        List<InformationEntryTranslation?>? Translations);
}
