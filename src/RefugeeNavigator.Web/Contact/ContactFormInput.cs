using System.ComponentModel.DataAnnotations;

namespace RefugeeNavigator.Web.Contact;

public sealed class ContactFormInput
{
    public string Topic { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ReplyEmail { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Honeypot { get; set; } = string.Empty;
}

public static class ContactFormValidation
{
    private static readonly HashSet<string> Topics = new(StringComparer.Ordinal)
    {
        "IncorrectOrOutdated", "BrokenSource", "TranslationError", "Accessibility",
        "Suggestion", "Collaboration", "GeneralEnquiry", "PrivacyData"
    };

    public static string? ErrorKey(ContactFormInput input)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (!string.IsNullOrWhiteSpace(input.Honeypot)) return "Contact.ValidationFailed";
        if (!Topics.Contains(input.Topic)) return "Contact.TopicRequired";
        if (string.IsNullOrWhiteSpace(input.Message)) return "Contact.MessageRequired";
        if (input.Message.Length > 5000) return "Contact.MessageTooLong";
        if (input.Name.Length > 100) return "Contact.NameTooLong";
        if (input.ReplyEmail.Length > 254) return "Contact.EmailTooLong";
        if (!string.IsNullOrWhiteSpace(input.ReplyEmail) && !new EmailAddressAttribute().IsValid(input.ReplyEmail))
            return "Contact.EmailInvalid";
        return null;
    }
}
