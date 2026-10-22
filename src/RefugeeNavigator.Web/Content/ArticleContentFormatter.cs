using System.Text.RegularExpressions;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Web.Content;

internal static class ArticleContentFormatter
{
    private static readonly Regex BlockSeparator = new(@"\r?\n[^\S\r\n]*(?:\r?\n)+", RegexOptions.CultureInvariant);
    private static readonly Regex InlineSourceToken = new(@"\[\[source:(?<id>[^|\]\r\n]+)\|(?<label>[^\]\r\n]*)\]\]", RegexOptions.CultureInvariant);

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
                blocks.Add(new ArticleContentBlock(ArticleContentBlockKind.Heading, block[3..].Trim(), [], []));
                continue;
            }

            var lines = block.Split('\n');
            if (lines.All(line => line.TrimEnd('\r').StartsWith("- ", StringComparison.Ordinal)) &&
                lines.All(line => !string.IsNullOrWhiteSpace(line.TrimEnd('\r')[2..])))
            {
                blocks.Add(new ArticleContentBlock(
                    ArticleContentBlockKind.UnorderedList,
                    string.Empty,
                    [],
                    lines.Select(line => ParseInline(line.TrimEnd('\r')[2..].Trim(), informationEntryId, sources)).ToArray()));
                continue;
            }

            blocks.Add(new ArticleContentBlock(
                ArticleContentBlockKind.Paragraph,
                block,
                ParseInline(block, informationEntryId, sources),
                []));
        }

        return blocks;
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
                result.Add(new ArticleContentText(text[position..match.Index]));
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
            result.Add(new ArticleContentText(text[position..]));
        }

        return result;
    }
}

internal enum ArticleContentBlockKind
{
    Paragraph,
    Heading,
    UnorderedList
}

internal sealed record ArticleContentBlock(
    ArticleContentBlockKind Kind,
    string Text,
    IReadOnlyList<ArticleContentInlinePart> InlineParts,
    IReadOnlyList<IReadOnlyList<ArticleContentInlinePart>> Items);

internal abstract record ArticleContentInlinePart;

internal sealed record ArticleContentText(string Value) : ArticleContentInlinePart;

internal sealed record ArticleContentSourceLink(string Label, Uri Url) : ArticleContentInlinePart;
