namespace RefugeeNavigator.Domain;

public sealed class OfficialSource
{
    public OfficialSource(Guid id, Guid informationEntryId, string title, Uri url, DateTimeOffset lastCheckedAt)
    {
        Id = id;
        InformationEntryId = informationEntryId;
        Title = title;
        Url = url;
        LastCheckedAt = lastCheckedAt;
    }

    public Guid Id { get; }

    public Guid InformationEntryId { get; }

    public string Title { get; }

    public Uri Url { get; }

    public DateTimeOffset LastCheckedAt { get; }
}
