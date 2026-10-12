namespace RefugeeNavigator.Domain;

public static class DomainModelValidator
{
    public static IReadOnlyList<string> Validate(Country model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var errors = new List<string>();
        ValidateId(model.Id, nameof(model.Id), errors);
        ValidateRequiredString(model.Name, nameof(model.Name), errors);
        ValidateSlug(model.Slug, errors);
        return errors.AsReadOnly();
    }

    public static IReadOnlyList<string> Validate(InformationCategory model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var errors = new List<string>();
        ValidateId(model.Id, nameof(model.Id), errors);
        ValidateRequiredString(model.Name, nameof(model.Name), errors);
        ValidateSlug(model.Slug, errors);
        return errors.AsReadOnly();
    }

    public static IReadOnlyList<string> Validate(InformationEntry model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var errors = new List<string>();
        ValidateId(model.Id, nameof(model.Id), errors);
        ValidateId(model.CountryId, nameof(model.CountryId), errors);
        ValidateId(model.CategoryId, nameof(model.CategoryId), errors);
        ValidateRequiredString(model.Title, nameof(model.Title), errors);
        ValidateRequiredString(model.Content, nameof(model.Content), errors);

        if (!Enum.IsDefined(model.VerificationStatus))
        {
            errors.Add("VerificationStatus must be a defined value.");
        }

        return errors.AsReadOnly();
    }

    public static IReadOnlyList<string> Validate(OfficialSource model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var errors = new List<string>();
        ValidateId(model.Id, nameof(model.Id), errors);
        ValidateId(model.InformationEntryId, nameof(model.InformationEntryId), errors);
        ValidateRequiredString(model.Title, nameof(model.Title), errors);

        if (model.Url is null)
        {
            errors.Add("Url is required.");
        }
        else if (!model.Url.IsAbsoluteUri)
        {
            errors.Add("Url must be absolute.");
        }
        else if (model.Url.Scheme is not "http" and not "https")
        {
            errors.Add("Url must use HTTP or HTTPS.");
        }

        if (model.LastCheckedAt == default)
        {
            errors.Add("LastCheckedAt must not be default.");
        }
        else if (model.LastCheckedAt > DateTimeOffset.UtcNow)
        {
            errors.Add("LastCheckedAt must not be in the future.");
        }

        return errors.AsReadOnly();
    }

    private static void ValidateId(Guid id, string propertyName, ICollection<string> errors)
    {
        if (id == Guid.Empty)
        {
            errors.Add($"{propertyName} must not be empty.");
        }
    }

    private static void ValidateRequiredString(string? value, string propertyName, ICollection<string> errors)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"{propertyName} is required.");
        }
    }

    private static void ValidateSlug(string? slug, ICollection<string> errors)
    {
        ValidateRequiredString(slug, nameof(Country.Slug), errors);
        if (string.IsNullOrWhiteSpace(slug))
        {
            return;
        }

        if (!IsValidSlug(slug))
        {
            errors.Add("Slug has an invalid format.");
        }
    }

    private static bool IsValidSlug(string slug)
    {
        if (!IsAsciiLetterOrDigit(slug[0]) || !IsAsciiLetterOrDigit(slug[^1]))
        {
            return false;
        }

        for (var index = 0; index < slug.Length; index++)
        {
            var character = slug[index];
            if (!IsAsciiLetterOrDigit(character) && character != '-')
            {
                return false;
            }

            if (character == '-' && index > 0 && slug[index - 1] == '-')
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsAsciiLetterOrDigit(char character) =>
        character is >= 'a' and <= 'z' or >= '0' and <= '9';
}
