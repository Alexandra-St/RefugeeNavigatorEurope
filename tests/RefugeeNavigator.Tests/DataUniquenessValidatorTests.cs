using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class DataUniquenessValidatorTests
{
    private static readonly Guid Id1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid Id2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid Id3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly DateTimeOffset CheckedAt = new(2020, 1, 2, 3, 4, 5, TimeSpan.Zero);

    [Fact]
    public void UniqueCountriesReturnNoErrors()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id2, "germany") };

        Assert.Empty(DataUniquenessValidator.Validate(countries));
    }

    [Fact]
    public void DuplicateCountryIdIsDetected()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id1, "germany") };

        Assert.Equal(new[] { "Country: duplicate Id." }, DataUniquenessValidator.Validate(countries));
    }

    [Fact]
    public void DuplicateCountrySlugIsDetected()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id2, "denmark") };

        Assert.Equal(new[] { "Country: duplicate Slug." }, DataUniquenessValidator.Validate(countries));
    }

    [Fact]
    public void CountrySlugDuplicatesAreCaseInsensitive()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id2, "Denmark") };

        Assert.Equal(new[] { "Country: duplicate Slug." }, DataUniquenessValidator.Validate(countries));
    }

    [Fact]
    public void ThreeCountriesWithSameIdProduceTwoErrors()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id1, "germany"), Country(Id1, "poland") };

        Assert.Equal(2, DataUniquenessValidator.Validate(countries).Count);
    }

    [Fact]
    public void UniqueInformationCategoriesReturnNoErrors()
    {
        var categories = new[] { Category(Id1, "employment"), Category(Id2, "housing") };

        Assert.Empty(DataUniquenessValidator.Validate(categories));
    }

    [Fact]
    public void DuplicateInformationCategoryIdIsDetected()
    {
        var categories = new[] { Category(Id1, "employment"), Category(Id1, "housing") };

        Assert.Equal(new[] { "InformationCategory: duplicate Id." }, DataUniquenessValidator.Validate(categories));
    }

    [Fact]
    public void DuplicateInformationCategorySlugIsDetected()
    {
        var categories = new[] { Category(Id1, "employment"), Category(Id2, "employment") };

        Assert.Equal(new[] { "InformationCategory: duplicate Slug." }, DataUniquenessValidator.Validate(categories));
    }

    [Fact]
    public void InformationCategorySlugDuplicatesAreCaseInsensitive()
    {
        var categories = new[] { Category(Id1, "housing"), Category(Id2, "Housing") };

        Assert.Equal(new[] { "InformationCategory: duplicate Slug." }, DataUniquenessValidator.Validate(categories));
    }

    [Fact]
    public void UniqueInformationEntriesReturnNoErrors()
    {
        var entries = new[] { Entry(Id1), Entry(Id2) };

        Assert.Empty(DataUniquenessValidator.Validate(entries));
    }

    [Fact]
    public void DuplicateInformationEntryIdIsDetected()
    {
        var entries = new[] { Entry(Id1), Entry(Id1) };

        Assert.Equal(new[] { "InformationEntry: duplicate Id." }, DataUniquenessValidator.Validate(entries));
    }

    [Fact]
    public void UniqueTranslationsForDifferentLanguagesReturnNoErrors()
    {
        var translations = new[] { Translation(Id1, "uk"), Translation(Id1, "en"), Translation(Id1, "ru") };

        Assert.Empty(DataUniquenessValidator.Validate(translations));
    }

    [Fact]
    public void DuplicateTranslationEntryAndLanguagePairIsDetected()
    {
        var translations = new[] { Translation(Id1, "uk"), Translation(Id1, "uk") };

        Assert.Equal(
            new[] { "InformationEntryTranslation: duplicate InformationEntryId and LanguageCode." },
            DataUniquenessValidator.Validate(translations));
    }

    [Fact]
    public void UniqueOfficialSourcesReturnNoErrorsAndAllowRepeatedUrls()
    {
        var url = new Uri("https://example.org/source");
        var sources = new[] { Source(Id1, "First", url), Source(Id2, "Second", url) };

        Assert.Empty(DataUniquenessValidator.Validate(sources));
    }

    [Fact]
    public void DuplicateOfficialSourceIdIsDetected()
    {
        var sources = new[] { Source(Id1, "First"), Source(Id1, "Second") };

        Assert.Equal(new[] { "OfficialSource: duplicate Id." }, DataUniquenessValidator.Validate(sources));
    }

    [Fact]
    public void EmptyCollectionsReturnNoErrors()
    {
        Assert.Empty(DataUniquenessValidator.Validate(Array.Empty<Country>()));
        Assert.Empty(DataUniquenessValidator.Validate(Array.Empty<InformationCategory>()));
        Assert.Empty(DataUniquenessValidator.Validate(Array.Empty<InformationEntry>()));
        Assert.Empty(DataUniquenessValidator.Validate(Array.Empty<InformationEntryTranslation>()));
        Assert.Empty(DataUniquenessValidator.Validate(Array.Empty<OfficialSource>()));
    }

    [Fact]
    public void NullCollectionsThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => DataUniquenessValidator.Validate((IEnumerable<Country>)null!));
        Assert.Throws<ArgumentNullException>(() => DataUniquenessValidator.Validate((IEnumerable<InformationCategory>)null!));
        Assert.Throws<ArgumentNullException>(() => DataUniquenessValidator.Validate((IEnumerable<InformationEntry>)null!));
        Assert.Throws<ArgumentNullException>(() => DataUniquenessValidator.Validate((IEnumerable<InformationEntryTranslation>)null!));
        Assert.Throws<ArgumentNullException>(() => DataUniquenessValidator.Validate((IEnumerable<OfficialSource>)null!));
    }

    [Fact]
    public void DuplicatePropertiesProduceAllErrorsInEncounterOrder()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id1, "DENMARK"), Country(Id1, "Denmark") };

        Assert.Equal(
            new[]
            {
                "Country: duplicate Id.",
                "Country: duplicate Slug.",
                "Country: duplicate Id.",
                "Country: duplicate Slug."
            },
            DataUniquenessValidator.Validate(countries));
    }

    [Fact]
    public void InputCollectionIsNotModified()
    {
        var countries = new[] { Country(Id1, "denmark"), Country(Id1, "Denmark") };
        var original = countries.ToArray();

        _ = DataUniquenessValidator.Validate(countries);

        Assert.Equal(original, countries);
    }

    [Fact]
    public void InputCollectionIsEnumeratedOnlyOnce()
    {
        var enumerationCount = 0;

        IEnumerable<Country> Countries()
        {
            if (++enumerationCount > 1)
            {
                throw new InvalidOperationException("The collection was enumerated more than once.");
            }

            yield return Country(Id1, "denmark");
            yield return Country(Id2, "germany");
        }

        Assert.Empty(DataUniquenessValidator.Validate(Countries()));
        Assert.Equal(1, enumerationCount);
    }

    [Fact]
    public void SameGuidAcrossDifferentModelTypesIsNotADuplicate()
    {
        Assert.Empty(DataUniquenessValidator.Validate(new[] { Country(Id1, "denmark") }));
        Assert.Empty(DataUniquenessValidator.Validate(new[] { Category(Id1, "denmark") }));
    }

    private static Country Country(Guid id, string slug) => new(id, slug, slug);

    private static InformationCategory Category(Guid id, string slug) => new(id, slug, slug);

    private static InformationEntry Entry(Guid id) => new(id, Id2, Id3);

    private static InformationEntryTranslation Translation(Guid entryId, string languageCode) => new(
        entryId, languageCode, "Title", "Summary", "Content", VerificationStatus.Verified, CheckedAt);

    private static OfficialSource Source(Guid id, string title, Uri? url = null) => new(
        id, Id2, title, url ?? new Uri("https://example.org/source"), CheckedAt);
}
