using System.Collections;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class DataRelationshipValidatorTests
{
    private static readonly Guid Id1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid Id2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid Id3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid Id4 = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly DateTimeOffset CheckedAt = new(2020, 1, 2, 3, 4, 5, TimeSpan.Zero);

    [Fact]
    public void ExistingReferencesReturnNoErrors()
    {
        var errors = DataRelationshipValidator.Validate(
            new[] { Country(Id1) },
            new[] { Category(Id2) },
            new[] { Entry(Id3, Id1, Id2) },
            new[] { Source(Id4, Id3) });

        Assert.Empty(errors);
    }

    [Fact]
    public void MissingCountryReferenceReturnsError()
    {
        var errors = DataRelationshipValidator.Validate(
            Array.Empty<Country>(),
            new[] { Category(Id2) },
            new[] { Entry(Id3, Id1, Id2) },
            Array.Empty<OfficialSource>());

        Assert.Equal(
            new[] { "InformationEntry: CountryId references a missing Country." },
            errors);
    }

    [Fact]
    public void MissingCategoryReferenceReturnsError()
    {
        var errors = DataRelationshipValidator.Validate(
            new[] { Country(Id1) },
            Array.Empty<InformationCategory>(),
            new[] { Entry(Id3, Id1, Id2) },
            Array.Empty<OfficialSource>());

        Assert.Equal(
            new[] { "InformationEntry: CategoryId references a missing InformationCategory." },
            errors);
    }

    [Fact]
    public void MissingEntryReferenceReturnsError()
    {
        var errors = DataRelationshipValidator.Validate(
            Array.Empty<Country>(),
            Array.Empty<InformationCategory>(),
            Array.Empty<InformationEntry>(),
            new[] { Source(Id4, Id3) });

        Assert.Equal(
            new[] { "OfficialSource: InformationEntryId references a missing InformationEntry." },
            errors);
    }

    [Fact]
    public void MultipleErrorsPreserveRequiredRecordAndPropertyOrder()
    {
        var entries = new[]
        {
            Entry(Id3, Id1, Id2),
            Entry(Id4, Id2, Id1)
        };
        var sources = new[] { Source(Id1, Id1), Source(Id2, Id2) };

        var errors = DataRelationshipValidator.Validate(
            Array.Empty<Country>(),
            Array.Empty<InformationCategory>(),
            entries,
            sources);

        Assert.Equal(
            new[]
            {
                "InformationEntry: CountryId references a missing Country.",
                "InformationEntry: CategoryId references a missing InformationCategory.",
                "InformationEntry: CountryId references a missing Country.",
                "InformationEntry: CategoryId references a missing InformationCategory.",
                "OfficialSource: InformationEntryId references a missing InformationEntry.",
                "OfficialSource: InformationEntryId references a missing InformationEntry."
            },
            errors);
    }

    [Fact]
    public void EmptyCollectionsReturnNoErrors()
    {
        Assert.Empty(DataRelationshipValidator.Validate(
            Array.Empty<Country>(),
            Array.Empty<InformationCategory>(),
            Array.Empty<InformationEntry>(),
            Array.Empty<OfficialSource>()));
    }

    [Fact]
    public void UnreferencedCountriesCategoriesAndEntriesAreAllowed()
    {
        var errors = DataRelationshipValidator.Validate(
            new[] { Country(Id1), Country(Id4) },
            new[] { Category(Id2), Category(Id3) },
            new[] { Entry(Id3, Id1, Id2) },
            Array.Empty<OfficialSource>());

        Assert.Empty(errors);
    }

    [Fact]
    public void EachNullCollectionThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => DataRelationshipValidator.Validate(
            null!, Array.Empty<InformationCategory>(), Array.Empty<InformationEntry>(), Array.Empty<OfficialSource>()));
        Assert.Throws<ArgumentNullException>(() => DataRelationshipValidator.Validate(
            Array.Empty<Country>(), null!, Array.Empty<InformationEntry>(), Array.Empty<OfficialSource>()));
        Assert.Throws<ArgumentNullException>(() => DataRelationshipValidator.Validate(
            Array.Empty<Country>(), Array.Empty<InformationCategory>(), null!, Array.Empty<OfficialSource>()));
        Assert.Throws<ArgumentNullException>(() => DataRelationshipValidator.Validate(
            Array.Empty<Country>(), Array.Empty<InformationCategory>(), Array.Empty<InformationEntry>(), null!));
    }

    [Fact]
    public void DuplicateIdentifiersDoNotProduceRelationshipErrors()
    {
        var errors = DataRelationshipValidator.Validate(
            new[] { Country(Id1), Country(Id1) },
            new[] { Category(Id2), Category(Id2) },
            new[] { Entry(Id3, Id1, Id2), Entry(Id3, Id1, Id2) },
            new[] { Source(Id4, Id3), Source(Id4, Id3) });

        Assert.Empty(errors);
    }

    [Fact]
    public void InputCollectionsAreNotModified()
    {
        var countries = new[] { Country(Id1) };
        var categories = new[] { Category(Id2) };
        var entries = new[] { Entry(Id3, Id1, Id2) };
        var sources = new[] { Source(Id4, Id3) };

        _ = DataRelationshipValidator.Validate(countries, categories, entries, sources);

        Assert.Single(countries);
        Assert.Single(categories);
        Assert.Single(entries);
        Assert.Single(sources);
        Assert.Equal(Id1, countries[0].Id);
        Assert.Equal(Id2, categories[0].Id);
        Assert.Equal(Id3, entries[0].Id);
        Assert.Equal(Id4, sources[0].Id);
    }

    [Fact]
    public void EachInputCollectionIsEnumeratedOnlyOnce()
    {
        var countries = new SingleEnumeration<Country>(Country(Id1));
        var categories = new SingleEnumeration<InformationCategory>(Category(Id2));
        var entries = new SingleEnumeration<InformationEntry>(Entry(Id3, Id1, Id2));
        var sources = new SingleEnumeration<OfficialSource>(Source(Id4, Id3));

        var errors = DataRelationshipValidator.Validate(countries, categories, entries, sources);

        Assert.Empty(errors);
        Assert.Equal(1, countries.EnumerationCount);
        Assert.Equal(1, categories.EnumerationCount);
        Assert.Equal(1, entries.EnumerationCount);
        Assert.Equal(1, sources.EnumerationCount);
    }

    private static Country Country(Guid id) => new(id, "Example Country", "example-country");

    private static InformationCategory Category(Guid id) => new(id, "Example Category", "example-category");

    private static InformationEntry Entry(Guid id, Guid countryId, Guid categoryId) =>
        new(id, countryId, categoryId, "Example Title", "Example Content", VerificationStatus.Verified);

    private static OfficialSource Source(Guid id, Guid entryId) =>
        new(id, entryId, "Example Source", new Uri("https://example.org/source"), CheckedAt);

    private sealed class SingleEnumeration<T>(T item) : IEnumerable<T>
    {
        public int EnumerationCount { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            if (++EnumerationCount > 1)
            {
                throw new InvalidOperationException("The collection was enumerated more than once.");
            }

            yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
