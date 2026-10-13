namespace RefugeeNavigator.Domain;

public sealed class InformationEntry
{
    public InformationEntry(Guid id, Guid countryId, Guid categoryId)
    {
        Id = id;
        CountryId = countryId;
        CategoryId = categoryId;
    }

    public Guid Id { get; }

    public Guid CountryId { get; }

    public Guid CategoryId { get; }
}
