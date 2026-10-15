using System.Text.RegularExpressions;

namespace RefugeeNavigator.Web.Content;

internal static class ArticleContentFormatter
{
    private static readonly Regex BlockSeparator = new(@"\r?\n[^\S\r\n]*(?:\r?\n)+", RegexOptions.CultureInvariant);

    public static IReadOnlyList<ArticleContentBlock> Parse(string? content)
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
                blocks.Add(new ArticleContentBlock(ArticleContentBlockKind.Heading, block[3..].Trim(), []));
                continue;
            }

            var lines = block.Split('\n');
            if (lines.All(line => line.TrimEnd('\r').StartsWith("- ", StringComparison.Ordinal)) &&
                lines.All(line => !string.IsNullOrWhiteSpace(line.TrimEnd('\r')[2..])))
            {
                blocks.Add(new ArticleContentBlock(
                    ArticleContentBlockKind.UnorderedList,
                    string.Empty,
                    lines.Select(line => line.TrimEnd('\r')[2..].Trim()).ToArray()));
                continue;
            }

            blocks.Add(new ArticleContentBlock(ArticleContentBlockKind.Paragraph, block, []));
        }

        return blocks;
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
    IReadOnlyList<string> Items);
