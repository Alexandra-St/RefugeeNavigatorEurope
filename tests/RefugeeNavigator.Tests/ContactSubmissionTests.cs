using System.Net;
using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Configuration;
using RefugeeNavigator.Web.Contact;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class ContactSubmissionTests
{
    [Fact]
    public void ValidationRequiresTopicAndMessageAndChecksOptionalEmailAndLengths()
    {
        Assert.Equal("Contact.TopicRequired", ContactFormValidation.ErrorKey(new ContactFormInput { Message = "Hello" }));
        Assert.Equal("Contact.MessageRequired", ContactFormValidation.ErrorKey(new ContactFormInput { Topic = "GeneralEnquiry" }));
        Assert.Equal("Contact.EmailInvalid", ContactFormValidation.ErrorKey(WithEmail(ValidInput(), "not-an-email")));
        Assert.Equal("Contact.MessageTooLong", ContactFormValidation.ErrorKey(WithMessage(ValidInput(), new string('x', 5001))));
        Assert.Equal("Contact.NameTooLong", ContactFormValidation.ErrorKey(WithName(ValidInput(), new string('x', 101))));
        Assert.Equal("Contact.EmailTooLong", ContactFormValidation.ErrorKey(WithEmail(ValidInput(), new string('a', 250) + "@example.test")));
        Assert.Equal("Contact.TopicRequired", ContactFormValidation.ErrorKey(new ContactFormInput { Topic = "other", Message = "Hello" }));
        Assert.Null(ContactFormValidation.ErrorKey(new ContactFormInput { Topic = "PrivacyData", Message = "Please tell me what data is held." }));
        Assert.Null(ContactFormValidation.ErrorKey(ValidInput()));
        Assert.Null(ContactFormValidation.ErrorKey(WithEmail(ValidInput(), string.Empty)));
    }

    [Fact]
    public async Task MissingFormIdDisablesSubmissionWithoutCallingProvider()
    {
        var handler = new RecordingHandler(_ => new HttpResponseMessage(HttpStatusCode.OK));
        var service = CreateService(handler, formId: null);

        Assert.False(service.IsAvailable);
        Assert.Equal(ContactSubmissionResult.Unavailable, await service.SubmitAsync(ValidInput(), "General enquiry"));
        Assert.Equal(0, handler.RequestCount);
    }

    [Fact]
    public async Task AcceptedProviderResponseReturnsAcceptedAndUsesConfiguredHttpsEndpoint()
    {
        var handler = new RecordingHandler(request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://formspree.io/f/testForm123", request.RequestUri?.AbsoluteUri);
            return new HttpResponseMessage(HttpStatusCode.OK);
        });
        var service = CreateService(handler, "testForm123");

        var result = await service.SubmitAsync(WithEmail(ValidInput(), "user@example.test"), "General enquiry");

        Assert.Equal(ContactSubmissionResult.Accepted, result);
        Assert.Equal(1, handler.RequestCount);
        Assert.DoesNotContain("@", handler.RequestUri ?? string.Empty);
    }

    [Fact]
    public async Task ProviderFailureReturnsFailureAndInvalidInputDoesNotSubmit()
    {
        var handler = new RecordingHandler(_ => new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
        var service = CreateService(handler, "testForm123");

        Assert.Equal(ContactSubmissionResult.Invalid,
            await service.SubmitAsync(WithEmail(ValidInput(), "invalid"), "General enquiry"));
        Assert.Equal(0, handler.RequestCount);
        Assert.Equal(ContactSubmissionResult.Failed, await service.SubmitAsync(ValidInput(), "General enquiry"));
        Assert.Equal(1, handler.RequestCount);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public void ContactPageResourcesAreComplete(string language)
    {
        var manager = new ResourceManager("RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);
        var culture = CultureInfo.GetCultureInfo(language);
        foreach (var key in new[] { "Contact.PageTitle", "Contact.MetaDescription", "Contact.Heading", "Contact.Introduction", "Contact.FormHeading", "Contact.Unavailable", "Contact.TopicLabel", "Contact.TopicPlaceholder", "Contact.Topic.IncorrectOrOutdated", "Contact.Topic.BrokenSource", "Contact.Topic.TranslationError", "Contact.Topic.Accessibility", "Contact.Topic.Suggestion", "Contact.Topic.Collaboration", "Contact.Topic.GeneralEnquiry", "Contact.Topic.PrivacyData", "Contact.PrivacyLinkPrefix", "Contact.PrivacyLink", "Contact.NameLabel", "Contact.EmailLabel", "Contact.WebsiteLabel", "Contact.MessageLabel", "Contact.PrivacyNotice", "Contact.SensitiveDataWarning", "Contact.Submit", "Contact.Sending", "Contact.Success", "Contact.Failure", "Contact.ValidationFailed", "Contact.TopicRequired", "Contact.MessageRequired", "Contact.MessageTooLong", "Contact.NameTooLong", "Contact.EmailTooLong", "Contact.EmailInvalid" })
            Assert.False(string.IsNullOrWhiteSpace(manager.GetString(key, culture)), $"Missing {key} for {language}");
        foreach (var key in new[] { "About.SupportTitle", "About.Support", "About.SupportVoluntary", "About.SupportCta" })
            Assert.False(string.IsNullOrWhiteSpace(manager.GetString(key, culture)), $"Missing {key} for {language}");
    }

    private static ContactSubmissionService CreateService(HttpMessageHandler handler, string? formId)
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Contact:FormId"] = formId
        }).Build();
        return new ContactSubmissionService(new HttpClient(handler), configuration);
    }

    private static ContactFormInput ValidInput() => new() { Topic = "GeneralEnquiry", Message = "Please update the page." };
    private static ContactFormInput WithEmail(ContactFormInput input, string email) { input.ReplyEmail = email; return input; }
    private static ContactFormInput WithMessage(ContactFormInput input, string message) { input.Message = message; return input; }
    private static ContactFormInput WithName(ContactFormInput input, string name) { input.Name = name; return input; }

    private sealed class RecordingHandler(Func<HttpRequestMessage, HttpResponseMessage> responder) : HttpMessageHandler
    {
        public int RequestCount { get; private set; }
        public string? RequestUri { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestCount++;
            RequestUri = request.RequestUri?.AbsoluteUri;
            return Task.FromResult(responder(request));
        }
    }
}
