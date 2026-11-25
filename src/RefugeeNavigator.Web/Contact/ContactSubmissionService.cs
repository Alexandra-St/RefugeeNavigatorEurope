using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace RefugeeNavigator.Web.Contact;

public enum ContactSubmissionResult { Accepted, Unavailable, Invalid, Failed }

public sealed class ContactSubmissionService(HttpClient httpClient, IConfiguration configuration)
{
    private static readonly Regex FormIdPattern = new("^[A-Za-z0-9]+$", RegexOptions.CultureInvariant | RegexOptions.Compiled);
    private readonly string? _formId = configuration["Contact:FormId"] is { } configuredId && FormIdPattern.IsMatch(configuredId)
        ? configuredId
        : null;

    public bool IsAvailable => _formId is not null;

    public async Task<ContactSubmissionResult> SubmitAsync(
        ContactFormInput input,
        string localizedTopic,
        CancellationToken cancellationToken = default)
    {
        if (ContactFormValidation.ErrorKey(input) is not null) return ContactSubmissionResult.Invalid;
        if (_formId is null) return ContactSubmissionResult.Unavailable;

        var payload = new Dictionary<string, string>
        {
            ["topic"] = localizedTopic,
            ["name"] = input.Name.Trim(),
            ["message"] = input.Message.Trim(),
            ["_gotcha"] = string.Empty
        };
        if (!string.IsNullOrWhiteSpace(input.ReplyEmail))
            payload["email"] = input.ReplyEmail.Trim();

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"https://formspree.io/f/{_formId}")
            {
                Content = JsonContent.Create(payload)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using var response = await httpClient.SendAsync(request, cancellationToken);
            return response.IsSuccessStatusCode ? ContactSubmissionResult.Accepted : ContactSubmissionResult.Failed;
        }
        catch (HttpRequestException)
        {
            return ContactSubmissionResult.Failed;
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return ContactSubmissionResult.Failed;
        }
    }
}
