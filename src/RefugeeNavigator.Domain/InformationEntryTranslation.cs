namespace RefugeeNavigator.Domain;

public sealed class InformationEntryTranslation
{
    public InformationEntryTranslation(
        Guid informationEntryId,
        string languageCode,
        string title,
        string summary,
        string content,
        VerificationStatus verificationStatus,
        DateTimeOffset lastUpdatedAt)
    {
        InformationEntryId = informationEntryId;
        LanguageCode = languageCode;
        Title = title;
        Summary = summary;
        Content = content;
        VerificationStatus = verificationStatus;
        LastUpdatedAt = lastUpdatedAt;
    }

    public Guid InformationEntryId { get; }

    public string LanguageCode { get; }

    public string Title { get; }

    public string Summary { get; }

    public string Content { get; }

    public VerificationStatus VerificationStatus { get; }

    public DateTimeOffset LastUpdatedAt { get; }
}
