using System.Globalization;

namespace RefugeeNavigator.ContentAudit;

public sealed record ContentAuditCommandLine(
    string DataDirectory,
    DateOnly AsOf,
    AuditThresholds Thresholds,
    string? OutputPath,
    bool ShowHelp)
{
    public static ContentAuditCommandLine Parse(string[] args, DateOnly currentUtcDate)
    {
        var values = new Dictionary<string, string>(StringComparer.Ordinal);
        var showHelp = false;
        for (var index = 0; index < args.Length; index++)
        {
            var argument = args[index];
            if (argument is "--help" or "-h")
            {
                showHelp = true;
                continue;
            }

            if (argument is not ("--data-dir" or "--as-of" or "--recent-days" or "--review-days" or "--output"))
            {
                throw new ArgumentException($"Unknown argument '{argument}'.");
            }

            if (++index >= args.Length || args[index].StartsWith("--", StringComparison.Ordinal))
            {
                throw new ArgumentException($"Argument '{argument}' requires a value.");
            }

            if (!values.TryAdd(argument, args[index]))
            {
                throw new ArgumentException($"Argument '{argument}' was supplied more than once.");
            }
        }

        var dataDirectory = values.GetValueOrDefault("--data-dir") ?? FindDefaultDataDirectory();
        var asOf = values.TryGetValue("--as-of", out var dateValue)
            ? DateOnly.TryParseExact(dateValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate)
                ? parsedDate
                : throw new ArgumentException("--as-of must use YYYY-MM-DD.")
            : currentUtcDate;
        var recentDays = ParseNonNegativeInt(values, "--recent-days", 30);
        var reviewDays = ParseNonNegativeInt(values, "--review-days", 90);
        var thresholds = new AuditThresholds(recentDays, reviewDays);
        thresholds.Validate();
        return new ContentAuditCommandLine(dataDirectory, asOf, thresholds, values.GetValueOrDefault("--output"), showHelp);
    }

    public static string Usage =>
        "Usage: dotnet run --project tools/RefugeeNavigator.ContentAudit -- [--data-dir PATH] [--as-of YYYY-MM-DD] [--recent-days N] [--review-days N] [--output PATH]\n" +
        "Defaults: repository Data directory; current UTC date; 30-day recent threshold; 90-day review-priority threshold.\n" +
        "If --output is omitted, the detailed Markdown report is printed after the console summary.";

    private static int ParseNonNegativeInt(IReadOnlyDictionary<string, string> values, string key, int defaultValue)
    {
        if (!values.TryGetValue(key, out var value))
        {
            return defaultValue;
        }

        if (!int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var result) || result < 0)
        {
            throw new ArgumentException($"{key} must be a non-negative integer.");
        }

        return result;
    }

    private static string FindDefaultDataDirectory()
    {
        for (var directory = new DirectoryInfo(Directory.GetCurrentDirectory()); directory is not null; directory = directory.Parent)
        {
            var candidate = Path.Combine(directory.FullName, "src", "RefugeeNavigator.Web", "Data");
            if (File.Exists(Path.Combine(candidate, "information-entries.json")))
            {
                return candidate;
            }
        }

        return Path.Combine(Directory.GetCurrentDirectory(), "src", "RefugeeNavigator.Web", "Data");
    }
}
