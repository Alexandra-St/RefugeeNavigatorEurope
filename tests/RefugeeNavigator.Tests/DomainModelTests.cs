using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class DomainModelTests
{
    [Fact]
    public void CountryConstructorPreservesPropertyValues()
    {
        var id = Guid.NewGuid();

        var country = new Country(id, "Example Country", "example-country");

        Assert.Equal(id, country.Id);
        Assert.Equal("Example Country", country.Name);
        Assert.Equal("example-country", country.Slug);
    }

    [Fact]
    public void InformationCategoryConstructorPreservesPropertyValues()
    {
        var id = Guid.NewGuid();

        var category = new InformationCategory(id, "Example Category", "example-category");

        Assert.Equal(id, category.Id);
        Assert.Equal("Example Category", category.Name);
        Assert.Equal("example-category", category.Slug);
    }

    [Fact]
    public void InformationEntryConstructorPreservesPropertyValues()
    {
        var id = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        var entry = new InformationEntry(id, countryId, categoryId, "Example Title", "Example Content");

        Assert.Equal(id, entry.Id);
        Assert.Equal(countryId, entry.CountryId);
        Assert.Equal(categoryId, entry.CategoryId);
        Assert.Equal("Example Title", entry.Title);
        Assert.Equal("Example Content", entry.Content);
    }

    [Fact]
    public void InformationEntryPreservesIndependentCountryAndCategoryIdentifiers()
    {
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var countryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var categoryId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var entry = new InformationEntry(id, countryId, categoryId, "Example Title", "Example Content");
        var swappedEntry = new InformationEntry(id, categoryId, countryId, "Example Title", "Example Content");

        Assert.Equal(countryId, entry.CountryId);
        Assert.Equal(categoryId, entry.CategoryId);
        Assert.NotEqual(entry.CountryId, entry.CategoryId);
        Assert.Equal(categoryId, swappedEntry.CountryId);
        Assert.Equal(countryId, swappedEntry.CategoryId);
    }

    [Fact]
    public void InformationEntryPropertiesHaveNoSetters()
    {
        foreach (var propertyName in new[] { "Id", "CountryId", "CategoryId", "Title", "Content" })
        {
            var property = typeof(InformationEntry).GetProperty(propertyName);

            Assert.NotNull(property);
            Assert.Null(property.GetSetMethod(nonPublic: true));
        }
    }

    [Theory]
    [InlineData(typeof(Country))]
    [InlineData(typeof(InformationCategory))]
    public void ModelPropertiesHaveNoSetters(Type modelType)
    {
        foreach (var propertyName in new[] { "Id", "Name", "Slug" })
        {
            var property = modelType.GetProperty(propertyName);

            Assert.NotNull(property);
            Assert.Null(property.GetSetMethod(nonPublic: true));
        }
    }
}
