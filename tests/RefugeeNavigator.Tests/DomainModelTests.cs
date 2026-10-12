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
