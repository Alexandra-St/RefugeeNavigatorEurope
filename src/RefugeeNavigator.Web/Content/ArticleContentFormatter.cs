using System.Text.RegularExpressions;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Web.Content;

internal static class ArticleContentFormatter
{
    private static readonly Regex BlockSeparator = new(@"\r?\n[^\S\r\n]*(?:\r?\n)+", RegexOptions.CultureInvariant);
    private static readonly Regex InlineSourceToken = new(@"\[\[source:(?<id>[^|\]\r\n]+)\|(?<label>[^\]\r\n]*)\]\]", RegexOptions.CultureInvariant);
    private static readonly Regex InlineBoldToken = new(@"\*\*(?<text>[^*\r\n]+)\*\*", RegexOptions.CultureInvariant);
    private static readonly Regex InlineCodeToken = new(@"`(?<text>[^`\r\n]+)`", RegexOptions.CultureInvariant);
    private static readonly Regex TableSeparatorCell = new(@"^:?-+:?$", RegexOptions.CultureInvariant);

    public static IReadOnlyList<ArticleContentBlock> Parse(
        string? content,
        Guid informationEntryId,
        IReadOnlyCollection<OfficialSource> sources)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return Array.Empty<ArticleContentBlock>();
        }

        var blocks = new List<ArticleContentBlock>();
        var sectionIndex = 0;
        foreach (var sourceBlock in BlockSeparator.Split(content))
        {
            var block = sourceBlock.Trim();
            if (block.Length == 0)
            {
                continue;
            }

            if (block.StartsWith("## ", StringComparison.Ordinal) &&
                block.IndexOfAny(['\r', '\n']) < 0 &&
                !string.IsNullOrWhiteSpace(block[3..]))
            {
                sectionIndex++;
                blocks.Add(new ArticleContentBlock(
                    ArticleContentBlockKind.Heading,
                    block[3..].Trim(),
                    [],
                    [],
                    $"article-section-{informationEntryId:N}-{sectionIndex}"));
                continue;
            }

            var lines = block.Split('\n');
            if (TryParseTable(lines, informationEntryId, sources, out var table))
            {
                blocks.Add(table);
                continue;
            }

            if (lines.All(line => line.TrimEnd('\r').StartsWith("- ", StringComparison.Ordinal)) &&
                lines.All(line => !string.IsNullOrWhiteSpace(line.TrimEnd('\r')[2..])))
            {
                blocks.Add(new ArticleContentBlock(
                    ArticleContentBlockKind.UnorderedList,
                    string.Empty,
                    [],
                    lines.Select(line => ParseInline(line.TrimEnd('\r')[2..].Trim(), informationEntryId, sources)).ToArray(),
                    null));
                continue;
            }

            blocks.Add(new ArticleContentBlock(
                ArticleContentBlockKind.Paragraph,
                block,
                ParseInline(block, informationEntryId, sources),
                [],
                null));
        }

        return blocks;
    }

    private static bool TryParseTable(
        string[] lines,
        Guid informationEntryId,
        IReadOnlyCollection<OfficialSource> sources,
        out ArticleContentBlock table)
    {
        table = default!;
        if (lines.Length < 3 || !TrySplitTableRow(lines[0], out var headers) || headers.Count < 2 ||
            !TrySplitTableRow(lines[1], out var separators) || separators.Count != headers.Count ||
            !lines.Skip(2).All(line => TrySplitTableRow(line, out var cells) && cells.Count == headers.Count))
        {
            return false;
        }

        var alignments = new List<ArticleContentColumnAlignment>(separators.Count);
        foreach (var separator in separators)
        {
            var value = separator.Trim();
            if (!TableSeparatorCell.IsMatch(value))
            {
                return false;
            }

            var left = value.StartsWith(':');
            var right = value.EndsWith(':');
            alignments.Add((left, right) switch
            {
                (true, true) => ArticleContentColumnAlignment.Center,
                (true, false) => ArticleContentColumnAlignment.Left,
                (false, true) => ArticleContentColumnAlignment.Right,
                _ => ArticleContentColumnAlignment.Default
            });
        }

        var rows = new List<IReadOnlyList<IReadOnlyList<ArticleContentInlinePart>>>();
        foreach (var line in lines.Skip(2))
        {
            TrySplitTableRow(line, out var cells);
            rows.Add(cells.Select(cell => ParseInline(cell, informationEntryId, sources)).ToArray());
        }

        table = new ArticleContentBlock(
            ArticleContentBlockKind.Table,
            string.Empty,
            [],
            [],
            null,
            headers.Select(cell => ParseInline(cell, informationEntryId, sources)).ToArray(),
            alignments,
            rows);
        return true;
    }

    private static bool TrySplitTableRow(string line, out List<string> cells)
    {
        var value = line.Trim().TrimEnd('\r').Trim();
        if (value.StartsWith('|')) value = value[1..];
        if (value.EndsWith('|')) value = value[..^1];
        cells = [];
        var cellStart = 0;
        var sourceTokenEnd = -1;
        for (var index = 0; index < value.Length; index++)
        {
            if (index >= sourceTokenEnd && value.AsSpan(index).StartsWith("[[source:", StringComparison.Ordinal))
            {
                var tokenEnd = value.IndexOf("]]", index, StringComparison.Ordinal);
                if (tokenEnd >= 0)
                {
                    sourceTokenEnd = tokenEnd + 2;
                }
            }

            if (value[index] == '|' && index >= sourceTokenEnd)
            {
                cells.Add(value[cellStart..index].Trim());
                cellStart = index + 1;
            }
        }

        cells.Add(value[cellStart..].Trim());
        return cells.Count >= 2;
    }

    private static IReadOnlyList<ArticleContentInlinePart> ParseInline(
        string text,
        Guid informationEntryId,
        IReadOnlyCollection<OfficialSource> sources)
    {
        var result = new List<ArticleContentInlinePart>();
        var position = 0;

        foreach (Match match in InlineSourceToken.Matches(text))
        {
            if (match.Index > position)
            {
                AddTextParts(result, text[position..match.Index]);
            }

            var label = match.Groups["label"].Value;
            if (Guid.TryParse(match.Groups["id"].Value, out var sourceId) &&
                !string.IsNullOrWhiteSpace(label))
            {
                var source = sources.FirstOrDefault(candidate =>
                    candidate.Id == sourceId &&
                    candidate.InformationEntryId == informationEntryId &&
                    candidate.Url.IsAbsoluteUri &&
                    candidate.Url.IsWellFormedOriginalString() &&
                    string.Equals(candidate.Url.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) &&
                    string.IsNullOrEmpty(candidate.Url.UserInfo) &&
                    !string.IsNullOrWhiteSpace(candidate.Url.Host));

                if (source is not null)
                {
                    result.Add(new ArticleContentSourceLink(label.Trim(), source.Url));
                    position = match.Index + match.Length;
                    continue;
                }
            }

            result.Add(new ArticleContentText(match.Value));
            position = match.Index + match.Length;
        }

        if (position < text.Length)
        {
            AddTextParts(result, text[position..]);
        }

        return result;
    }

    private static void AddTextParts(List<ArticleContentInlinePart> result, string text)
    {
        var position = 0;
        foreach (Match match in InlineCodeToken.Matches(text))
        {
            if (match.Index > position)
            {
                AddBoldParts(result, text[position..match.Index]);
            }

            result.Add(new ArticleContentCode(match.Groups["text"].Value));
            position = match.Index + match.Length;
        }

        if (position < text.Length)
        {
            AddBoldParts(result, text[position..]);
        }
    }

    private static void AddBoldParts(List<ArticleContentInlinePart> result, string text)
    {
        var position = 0;
        foreach (Match match in InlineBoldToken.Matches(text))
        {
            if (match.Index > position)
            {
                result.Add(new ArticleContentText(text[position..match.Index]));
            }

            result.Add(new ArticleContentStrong(match.Groups["text"].Value));
            position = match.Index + match.Length;
        }

        if (position < text.Length)
        {
            result.Add(new ArticleContentText(text[position..]));
        }
    }
}

internal enum ArticleContentBlockKind
{
    Paragraph,
    Heading,
    UnorderedList,
    Table
}

internal enum ArticleContentColumnAlignment { Default, Left, Right, Center }

internal sealed record ArticleContentBlock(
    ArticleContentBlockKind Kind,
    string Text,
    IReadOnlyList<ArticleContentInlinePart> InlineParts,
    IReadOnlyList<IReadOnlyList<ArticleContentInlinePart>> Items,
    string? AnchorId,
    IReadOnlyList<IReadOnlyList<ArticleContentInlinePart>>? TableHeaders = null,
    IReadOnlyList<ArticleContentColumnAlignment>? TableAlignments = null,
    IReadOnlyList<IReadOnlyList<IReadOnlyList<ArticleContentInlinePart>>>? TableRows = null);

internal abstract record ArticleContentInlinePart;

internal sealed record ArticleContentText(string Value) : ArticleContentInlinePart;

internal sealed record ArticleContentStrong(string Value) : ArticleContentInlinePart;

internal sealed record ArticleContentCode(string Value) : ArticleContentInlinePart;

internal sealed record ArticleContentSourceLink(string Label, Uri Url) : ArticleContentInlinePart;
