using RefugeeNavigator.ContentAudit;

try
{
    var commandLine = ContentAuditCommandLine.Parse(args, DateOnly.FromDateTime(DateTime.UtcNow));
    if (commandLine.ShowHelp)
    {
        Console.WriteLine(ContentAuditCommandLine.Usage);
        return 0;
    }

    var data = await new ContentAuditCatalogLoader().LoadAsync(commandLine.DataDirectory);
    var report = new ContentMaintenanceAuditor().CreateReport(data, commandLine.AsOf, commandLine.Thresholds);
    Console.WriteLine(ContentAuditReportFormatter.ToConsoleSummary(report));

    var markdown = ContentAuditReportFormatter.ToMarkdown(report);
    if (commandLine.OutputPath is null)
    {
        Console.WriteLine();
        Console.WriteLine(markdown);
    }
    else
    {
        var outputPath = Path.GetFullPath(commandLine.OutputPath);
        await File.WriteAllTextAsync(outputPath, markdown);
        Console.WriteLine($"Detailed Markdown report written to {outputPath}");
    }

    return 0;
}
catch (Exception exception) when (exception is ArgumentException or IOException or System.Text.Json.JsonException)
{
    Console.Error.WriteLine($"Content audit failed: {exception.Message}");
    Console.Error.WriteLine(ContentAuditCommandLine.Usage);
    return 1;
}
