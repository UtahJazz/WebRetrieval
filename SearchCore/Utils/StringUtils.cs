using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchCore.Utils
{
    public static class StringUtils
    {
        public static IEnumerable<string> SplitByLines(string data)
        {
            return data.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
        }

        public static IEnumerable<string> SplitByWord(string data)
        {
            var regexp = new Regex(@"(\n|\r\n| |\t)");

            return regexp.Split(data).Select(x => x.Trim()).Where(x => !String.IsNullOrWhiteSpace(x));
        }

        public static IEnumerable<string> SplitByLowerWord(string data)
        {
            return SplitByWord(data).Select(x => x.ToLower());
        }
    }
}
