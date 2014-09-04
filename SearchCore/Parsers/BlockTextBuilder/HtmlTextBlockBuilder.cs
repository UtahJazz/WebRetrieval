using System;
using System.Linq;
using System.Text;

namespace SearchCore.Parsers.BlockTextBuilder
{
    public sealed class HtmlTextBlockBuilder : IBlockTextBuilder
    {
        public void AppendTextBlock(string text)
        {
            _stringBuilder.Append(text);
            _stringBuilder.Append(Separator);
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }

        public void Clear()
        {
            _stringBuilder.Clear();
        }

        public string[] SplitByBlocks(string text)
        {
            return 
                text
                    .Split(new [] { Separator }, StringSplitOptions.None)
                    .Where(x => x.Trim() != string.Empty)
                    .ToArray();
        }

        private const string Separator = "><%'";
        private readonly StringBuilder _stringBuilder = new StringBuilder();
    }
}
