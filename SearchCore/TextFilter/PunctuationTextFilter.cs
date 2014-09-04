using System.Text.RegularExpressions;

namespace SearchCore.TextFilter
{
    public sealed class PunctuationTextFilter : ITextFilter
    {
        public string Filter(string text)
        {
            return Regex.Replace(text, @"[^\w\s]", " ");
        }
    }
}
