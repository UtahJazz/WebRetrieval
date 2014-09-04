using System.Collections.Generic;
using System.Linq;
using SearchCore.Microtest;
using SearchCore.Utils;

namespace SearchCore.Parsers
{
    public static class MicrotestParser
    {
        public static MicrotestData Parse(string content)
        {
            var microtest = new Dictionary<string, string>();

            foreach (var trimmedLine in StringUtils.SplitByLines(content).Select(line => line.Trim()))
            {
                if (trimmedLine.StartsWith(OggPrefix))
                {
                    microtest.Add(GetName(trimmedLine), GetValue(trimmedLine));
                }

                if(trimmedLine.StartsWith(HtmlUtils.HeadCloseTag))
                {
                    break;
                }
            }

            return new MicrotestData(microtest);
        }

        private static string GetName(string line)
        {
            var attribute = HtmlUtils.GetAttributeValue(line, "name");

            return attribute.Replace("og:", "");
        }
            
        private static string GetValue(string line)
        {
            return HtmlUtils.GetAttributeValue(line, "content");
        }

        private const string OggPrefix = "<meta name=\"og:";
    }
}
