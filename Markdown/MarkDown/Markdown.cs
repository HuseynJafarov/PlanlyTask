using System;
using System.Text.RegularExpressions;

namespace PlanlyTask.MarkDown
{
    public static class Markdown
    {
        // Wraps the given text with the specified HTML tag
        private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

        // Parses the markdown text to replace custom delimiters with HTML tags
        private static string Parse(string markdown, string delimiter, string tag)
        {
            var pattern = $"{delimiter}(.+?){delimiter}";
            var replacement = $"<{tag}>$1</{tag}>";
            return Regex.Replace(markdown, pattern, replacement);
        }

        // Parses bold text
        private static string ParseBold(string markdown) => Parse(markdown, "__", "strong");

        // Parses italic text
        private static string ParseItalic(string markdown) => Parse(markdown, "_", "em");

        // Parses text to handle both italic and bold formatting
        private static string ParseText(string markdown, bool inList)
        {
            var parsedText = ParseBold(ParseItalic(markdown));
            return inList ? parsedText : Wrap(parsedText, "p");
        }

        // Extracts header level and parses it
        private static string ParseHeader(string markdown, bool inList, out bool inListAfter)
        {
            int headerLevel = GetHeaderLevel(markdown);
            if (headerLevel > 0)
            {
                inListAfter = inList;
                string headerContent = markdown.Substring(headerLevel).Trim();
                return inList
                    ? $"</ul><h{headerLevel}>{headerContent}</h{headerLevel}>"
                    : $"<h{headerLevel}>{headerContent}</h{headerLevel}>";
            }
            inListAfter = inList;
            return null;
        }

        // Determines the level of the header based on the number of '#' characters
        private static int GetHeaderLevel(string markdown)
        {
            int level = 0;
            while (level < markdown.Length && markdown[level] == '#') level++;
            return level;
        }

        // Parses list items and wraps them in <li> tags
        private static string ParseListItem(string markdown, bool inList, out bool inListAfter)
        {
            if (markdown.TrimStart().StartsWith("*"))
            {
                var itemContent = ParseText(markdown.Substring(1).Trim(), true);
                var result = Wrap(itemContent, "li");
                inListAfter = true;
                return inList ? result : $"<ul>{result}";
            }
            inListAfter = inList;
            return null;
        }

        // Parses individual lines to determine their type (header, list item, or paragraph)
        private static string ParseLine(string markdown, bool inList, out bool inListAfter)
        {
            // Handle empty lines as paragraphs
            if (string.IsNullOrWhiteSpace(markdown))
            {
                inListAfter = false;
                return inList ? "</ul><p></p>" : "<p></p>";
            }

            var result = ParseHeader(markdown, inList, out inListAfter)
                          ?? ParseListItem(markdown, inList, out inListAfter)
                          ?? ParseText(markdown, inList);

            if (result == null) throw new ArgumentException("Invalid markdown");
            return result;
        }

        // Parses the entire markdown string
        public static string Parse(string markdown)
        {
            var lines = markdown.Split('\n');
            var result = "";
            var inList = false;

            foreach (var line in lines)
            {
                var lineResult = ParseLine(line, inList, out inList);
                result += lineResult;
            }

            if (inList) result += "</ul>";
            return result;
        }
    }

}
