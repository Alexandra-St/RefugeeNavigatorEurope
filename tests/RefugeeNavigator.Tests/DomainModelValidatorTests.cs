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
        var entry = new InformationEntry(
            Guid.Empty,
            Guid.Empty,
            Guid.Empty,
            "Title",
            "Content",
            VerificationStatus.NeedsReview);

        var errors = DomainModelValidator.Validate(entry);

        Assert.Contains("Id must not be empty.", errors);
        Assert.Contains("CountryId must not be empty.", errors);
        Assert.Contains("CategoryId must not be empty.", errors);
    }

    [Theory]
    [InlineData("", "Content", "Title is required.")]
    [InlineData("Title", "   ", "Content is required.")]
    public void InformationEntryWithMissingTextReturnsError(string title, string content, string expectedError)
    {
        var entry = new InformationEntry(
            ValidId, ValidCountryId, ValidCategoryId, title, content, VerificationStatus.Verified);

        Assert.Contains(expectedError, DomainModelValidator.Validate(entry));
    }

    [Fact]
    public void InformationEntryWithUndefinedVerificationStatusReturnsError()
    {
        var entry = new InformationEntry(
            ValidId, ValidCountryId, ValidCategoryId, "Title", "Content", (VerificationStatus)99);

        Assert.Contains("VerificationStatus must be a defined value.", DomainModelValidator.Validate(entry));
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
        Assert.Throws<ArgumentNullException>(() => DomainModelValidator.Validate((OfficialSource)null!));
    }

    [Fact]
    public void MultipleInvalidFieldsProduceMultipleErrors()
    {
        var entry = new InformationEntry(
            Guid.Empty,
            Guid.Empty,
            Guid.Empty,
            " ",
            "",
            (VerificationStatus)99);

        var errors = DomainModelValidator.Validate(entry);

        Assert.Equal(6, errors.Count);
        Assert.Contains("Id must not be empty.", errors);
        Assert.Contains("CountryId must not be empty.", errors);
        Assert.Contains("CategoryId must not be empty.", errors);
        Assert.Contains("Title is required.", errors);
        Assert.Contains("Content is required.", errors);
        Assert.Contains("VerificationStatus must be a defined value.", errors);
    }

    private static InformationEntry CreateInformationEntry() => new(
        ValidId,
        ValidCountryId,
        ValidCategoryId,
        "Title",
        "Content",
        VerificationStatus.Verified);

    private static OfficialSource CreateOfficialSource() => new(
        ValidId,
        ValidEntryId,
        "Source",
        new Uri("https://example.org/source"),
        ValidTimestamp);
}
