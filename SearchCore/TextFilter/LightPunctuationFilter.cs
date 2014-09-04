using System.Text.RegularExpressions;

namespace SearchCore.TextFilter
{
    public sealed class LightPunctuationFilter : ITextFilter
    {
        public string Filter(string text)
        {
            return _pattern.Replace(text, " ");
        }

        private readonly Regex _pattern = new Regex("[.,:;^?!{}]");
    }
}
