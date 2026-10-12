namespace RefugeeNavigator.Domain;

public sealed class Country
{
    public Country(Guid id, string name, string slug)
    {
        Id = id;
        Name = name;
        Slug = slug;
    }

    public Guid Id { get; }

    public string Name { get; }

    public string Slug { get; }
}
