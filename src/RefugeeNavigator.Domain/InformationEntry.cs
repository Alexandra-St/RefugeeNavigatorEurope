namespace RefugeeNavigator.Domain;

public sealed class InformationEntry
{
    public InformationEntry(Guid id, Guid countryId, Guid categoryId, string title, string content)
    {
        Id = id;
        CountryId = countryId;
        CategoryId = categoryId;
        Title = title;
        Content = content;
    }

    public Guid Id { get; }

    public Guid CountryId { get; }

    public Guid CategoryId { get; }

    public string Title { get; }

    public string Content { get; }
}
