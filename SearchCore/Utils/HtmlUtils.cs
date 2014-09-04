using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchCore.Utils
{
    public static class HtmlUtils
    {
        public static string GetAttributeValue(string line, string attribute)
        {
            var wordBag = new List<string>();

            foreach (var word in StringUtils.SplitByWord(line).Select(x => x.Split('=')))
            {
                wordBag.AddRange(word);
            }

            wordBag = wordBag.Select(x => x.Replace(CloseTag, String.Empty).Trim(QuotSymbols)).ToList();

            for (var i = 0; i < wordBag.Count; i++)
            {
                var word = wordBag[i];
                if (word.StartsWith(attribute))
                {
                    return wordBag[i + 1];
                }
            }

            throw new KeyNotFoundException("There is no attribute: " + attribute);
        }

        public static string CleanTags(string processLine)
        {
            return Regex.Replace(processLine, StartTagRegexp, " ");
        }

        public static string TagPrefix = "tag:";

        public const string HeadCloseTag = "</head>";

        public const string OpenTag = "<";
        public const string CloseTag = "/>";
        public const string DivTag = "div";
        public const string SpanTag = "span";
        public const string LinkTag = "a";
        public const string MainTitleTag = "h1";

        private static readonly char[] QuotSymbols = { '\'', '\"' };
        private const string StartTagRegexp = @"<[^>]*>";
    }
}
