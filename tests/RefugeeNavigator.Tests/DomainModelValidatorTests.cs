using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class DomainModelValidatorTests
{
    private static readonly Guid ValidId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid ValidCountryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid ValidCategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid ValidEntryId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly DateTimeOffset ValidTimestamp = new(2020, 1, 2, 3, 4, 5, TimeSpan.Zero);

    [Fact]
    public void ValidCountryReturnsNoErrors()
    {
        Assert.Empty(DomainModelValidator.Validate(new Country(ValidId, "Denmark", "denmark")));
    }

    [Fact]
    public void CountryWithEmptyIdReturnsError()
    {
        var errors = DomainModelValidator.Validate(new Country(Guid.Empty, "Denmark", "denmark"));

        Assert.Contains("Id must not be empty.", errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CountryWithMissingNameReturnsError(string name)
    {
        var errors = DomainModelValidator.Validate(new Country(ValidId, name, "denmark"));

        Assert.Contains("Name is required.", errors);
    }

    [Theory]
    [InlineData("Denmark")]
    [InlineData("-denmark")]
    [InlineData("denmark-")]
    [InlineData("denmark--test")]
    [InlineData("denmark test")]
    [InlineData("denmark_test")]
    [InlineData("dénmark")]
    public void CountryWithInvalidSlugReturnsError(string slug)
    {
        var errors = DomainModelValidator.Validate(new Country(ValidId, "Denmark", slug));

        Assert.Contains("Slug has an invalid format.", errors);
    }

    [Fact]
    public void ValidInformationCategoryReturnsNoErrors()
    {
        Assert.Empty(DomainModelValidator.Validate(
            new InformationCategory(ValidId, "Temporary Protection", "temporary-protection")));
    }

    [Fact]
    public void InformationCategoryWithEmptyIdReturnsError()
    {
        var errors = DomainModelValidator.Validate(
            new InformationCategory(Guid.Empty, "Temporary Protection", "temporary-protection"));

        Assert.Contains("Id must not be empty.", errors);
    }

    [Fact]
    public void InformationCategoryWithInvalidSlugReturnsError()
    {
        var errors = DomainModelValidator.Validate(
            new InformationCategory(ValidId, "Temporary Protection", "Temporary-Protection"));

        Assert.Contains("Slug has an invalid format.", errors);
    }

    [Fact]
    public void ValidInformationEntryReturnsNoErrors()
    {
        Assert.Empty(DomainModelValidator.Validate(CreateInformationEntry()));
    }

    [Fact]
    public void InformationEntryWithEmptyIdentifiersReturnsErrors()
    {
        var entry = new InformationEntry(Guid.Empty, Guid.Empty, Guid.Empty);

        var errors = DomainModelValidator.Validate(entry);

        Assert.Contains("Id must not be empty.", errors);
        Assert.Contains("CountryId must not be empty.", errors);
        Assert.Contains("CategoryId must not be empty.", errors);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public void SupportedTranslationLanguageCodesReturnNoErrors(string languageCode)
    {
        Assert.Empty(DomainModelValidator.Validate(CreateTranslation(languageCode)));
    }

    [Theory]
    [InlineData("de")]
    [InlineData("UK")]
    public void UnsupportedTranslationLanguageCodeReturnsError(string languageCode)
    {
        var errors = DomainModelValidator.Validate(CreateTranslation(languageCode));

        Assert.Contains("LanguageCode must be one of the supported values: uk, en, ru.", errors);
    }

    [Fact]
    public void TranslationWithMissingLanguageCodeReturnsRequiredError()
    {
        var translation = CreateTranslation(languageCode: " ");

        Assert.Contains("LanguageCode is required.", DomainModelValidator.Validate(translation));
    }

    [Fact]
    public void TranslationWithEmptyEntryIdReturnsError()
    {
        var translation = new InformationEntryTranslation(
            Guid.Empty, "uk", "Title", "Summary", "Content", VerificationStatus.NeedsReview, ValidTimestamp);

        Assert.Contains("InformationEntryId must not be empty.", DomainModelValidator.Validate(translation));
    }

    [Theory]
    [InlineData("", "Summary", "Content", "Title is required.")]
    [InlineData("Title", " ", "Content", "Summary is required.")]
    [InlineData("Title", "Summary", "", "Content is required.")]
    public void TranslationWithMissingTextReturnsError(
        string title,
        string summary,
        string content,
        string expectedError)
    {
        var translation = new InformationEntryTranslation(
            ValidEntryId, "uk", title, summary, content, VerificationStatus.Verified, ValidTimestamp);

        Assert.Contains(expectedError, DomainModelValidator.Validate(translation));
    }

    [Fact]
    public void TranslationWithUndefinedVerificationStatusReturnsError()
    {
        var translation = CreateTranslation(verificationStatus: (VerificationStatus)99);

        Assert.Contains("VerificationStatus must be a defined value.", DomainModelValidator.Validate(translation));
    }

    [Fact]
    public void TranslationWithDefaultLastUpdatedAtReturnsError()
    {
        var translation = CreateTranslation(lastUpdatedAt: new DateTimeOffset());

        Assert.Contains("LastUpdatedAt must not be default.", DomainModelValidator.Validate(translation));
    }

    [Fact]
    public void TranslationWithFutureLastUpdatedAtReturnsError()
    {
        var translation = CreateTranslation(lastUpdatedAt: new DateTimeOffset(2999, 1, 1, 0, 0, 0, TimeSpan.Zero));

        Assert.Contains("LastUpdatedAt must not be in the future.", DomainModelValidator.Validate(translation));
    }

    [Fact]
    public void ValidOfficialSourceReturnsNoErrors()
    {
        Assert.Empty(DomainModelValidator.Validate(CreateOfficialSource()));
    }

    [Fact]
    public void OfficialSourceWithEmptyIdentifiersReturnsErrors()
    {
        var source = new OfficialSource(
            Guid.Empty, Guid.Empty, "Source", new Uri("https://example.org"), ValidTimestamp);

        var errors = DomainModelValidator.Validate(source);

        Assert.Contains("Id must not be empty.", errors);
        Assert.Contains("InformationEntryId must not be empty.", errors);
    }

    [Fact]
    public void OfficialSourceWithNullUrlReturnsError()
    {
        var source = new OfficialSource(ValidId, ValidEntryId, "Source", null!, ValidTimestamp);

        Assert.Contains("Url is required.", DomainModelValidator.Validate(source));
    }

    [Fact]
    public void OfficialSourceWithRelativeUrlReturnsError()
    {
        var source = new OfficialSource(
            ValidId, ValidEntryId, "Source", new Uri("relative/path", UriKind.Relative), ValidTimestamp);

        Assert.Contains("Url must be absolute.", DomainModelValidator.Validate(source));
    }

    [Fact]
    public void OfficialSourceWithNonHttpUrlReturnsError()
    {
        var source = new OfficialSource(
            ValidId, ValidEntryId, "Source", new Uri("mailto:source@example.org"), ValidTimestamp);

        Assert.Contains("Url must use HTTP or HTTPS.", DomainModelValidator.Validate(source));
    }

    [Fact]
    public void OfficialSourceWithDefaultTimestampReturnsError()
    {
        var source = new OfficialSource(
            ValidId, ValidEntryId, "Source", new Uri("https://example.org"), default);

        Assert.Contains("LastCheckedAt must not be default.", DomainModelValidator.Validate(source));
    }

    [Fact]
    public void OfficialSourceWithFutureTimestampReturnsError()
    {
        var futureTimestamp = new DateTimeOffset(2999, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var source = new OfficialSource(
            ValidId, ValidEntryId, "Source", new Uri("https://example.org"), futureTimestamp);

        Assert.Contains("LastCheckedAt must not be in the future.", DomainModelValidator.Validate(source));
    }

    [Fact]
    public void NullModelsThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => DomainModelValidator.Validate((Country)null!));
        Assert.Throws<ArgumentNullException>(() => DomainModelValidator.Validate((InformationCategory)null!));
        Assert.Throws<ArgumentNullException>(() => DomainModelValidator.Validate((InformationEntry)null!));
        Assert.Throws<ArgumentNullException>(() => DomainModelValidator.Validate((InformationEntryTranslation)null!));
        Assert.Throws<ArgumentNullException>(() => DomainModelValidator.Validate((OfficialSource)null!));
    }

    [Fact]
    public void MultipleInvalidFieldsProduceMultipleErrors()
    {
        var entry = new InformationEntry(Guid.Empty, Guid.Empty, Guid.Empty);

        var errors = DomainModelValidator.Validate(entry);

        Assert.Equal(3, errors.Count);
        Assert.Contains("Id must not be empty.", errors);
        Assert.Contains("CountryId must not be empty.", errors);
        Assert.Contains("CategoryId must not be empty.", errors);
    }

    [Fact]
    public void TranslationWithMultipleInvalidFieldsProducesMultipleErrors()
    {
        var translation = new InformationEntryTranslation(
            Guid.Empty, "de", " ", "", " ", (VerificationStatus)99, default);

        var errors = DomainModelValidator.Validate(translation);

        Assert.Equal(7, errors.Count);
        Assert.Contains("InformationEntryId must not be empty.", errors);
        Assert.Contains("LanguageCode must be one of the supported values: uk, en, ru.", errors);
        Assert.Contains("Title is required.", errors);
        Assert.Contains("Summary is required.", errors);
        Assert.Contains("Content is required.", errors);
        Assert.Contains("VerificationStatus must be a defined value.", errors);
        Assert.Contains("LastUpdatedAt must not be default.", errors);
    }

    private static InformationEntry CreateInformationEntry() => new(
        ValidId,
        ValidCountryId,
        ValidCategoryId);

    private static InformationEntryTranslation CreateTranslation(
        string languageCode = "uk",
        VerificationStatus verificationStatus = VerificationStatus.Verified,
        DateTimeOffset? lastUpdatedAt = null) => new(
            ValidEntryId,
            languageCode,
            "Title",
            "Summary",
            "Content",
            verificationStatus,
            lastUpdatedAt ?? ValidTimestamp);

    private static OfficialSource CreateOfficialSource() => new(
        ValidId,
        ValidEntryId,
        "Source",
        new Uri("https://example.org/source"),
        ValidTimestamp);
}
