using System.Text.Json;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public class JsonSerializationTests
{
    [Fact]
    public void CountryRoundTripsThroughJson()
    {
        var model = new Country(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "Example Country",
            "example-country");

        var json = JsonSerializer.Serialize(model);
        var result = JsonSerializer.Deserialize<Country>(json);

        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
        Assert.Equal(model.Name, result.Name);
        Assert.Equal(model.Slug, result.Slug);
    }

    [Fact]
    public void InformationCategoryRoundTripsThroughJson()
    {
        var model = new InformationCategory(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Example Category",
            "example-category");

        var json = JsonSerializer.Serialize(model);
        var result = JsonSerializer.Deserialize<InformationCategory>(json);

        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
        Assert.Equal(model.Name, result.Name);
        Assert.Equal(model.Slug, result.Slug);
    }

    [Theory]
    [InlineData(VerificationStatus.NeedsReview)]
    [InlineData(VerificationStatus.Verified)]
    [InlineData(VerificationStatus.Outdated)]
    public void InformationEntryRoundTripsThroughJson(VerificationStatus verificationStatus)
    {
        var model = new InformationEntry(
            Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Guid.Parse("55555555-5555-5555-5555-555555555555"),
            "Example Title",
            "Example Content",
            verificationStatus);

        var json = JsonSerializer.Serialize(model);
        var result = JsonSerializer.Deserialize<InformationEntry>(json);

        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
        Assert.Equal(model.CountryId, result.CountryId);
        Assert.Equal(model.CategoryId, result.CategoryId);
        Assert.Equal(model.Title, result.Title);
        Assert.Equal(model.Content, result.Content);
        Assert.Equal(model.VerificationStatus, result.VerificationStatus);
    }

    [Fact]
    public void OfficialSourceRoundTripsThroughJsonAndPreservesTimestampOffset()
    {
        var model = new OfficialSource(
            Guid.Parse("66666666-6666-6666-6666-666666666666"),
            Guid.Parse("77777777-7777-7777-7777-777777777777"),
            "Example Source",
            new Uri("https://example.org/source"),
            new DateTimeOffset(2026, 10, 12, 14, 45, 30, TimeSpan.FromHours(5.5)));

        var json = JsonSerializer.Serialize(model);
        var result = JsonSerializer.Deserialize<OfficialSource>(json);

        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
        Assert.Equal(model.InformationEntryId, result.InformationEntryId);
        Assert.Equal(model.Title, result.Title);
        Assert.Equal(model.Url, result.Url);
        Assert.Equal(model.LastCheckedAt, result.LastCheckedAt);
        Assert.Equal(model.LastCheckedAt.Offset, result.LastCheckedAt.Offset);
    }
}
