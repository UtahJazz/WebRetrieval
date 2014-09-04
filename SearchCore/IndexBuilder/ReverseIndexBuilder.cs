using System.Linq;
using SearchCore.Index;
using SearchCore.Parsers;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.IndexBuilder
{
    public sealed class ReverseIndexBuilder : IReverseIndexBuilder
    {
        public ReverseIndexBuilder(
            IIndex index, 
            ITextFilter textFilter, 
            ITextFilter tagTaxtFilter)
        {
            _index = index;
            _textFilter = textFilter;
            _tagTaxtFilter = tagTaxtFilter;
        }

        public void AppendFile(PageContent fileContent, int fileId)
        {
            _index.AppendDocument();

            foreach (var word in fileContent
                .Paragraphs
                .SelectMany(
                    paragraph => StringUtils.SplitByWord(
                        _textFilter.Filter(paragraph))))
            {
                _index.AppendWord(word, fileId);
            }

            foreach (var tag in fileContent
                .Tags
                .Select(
                    tag => 
                        _tagTaxtFilter.Filter(tag).ToLower()))
            {
                _index.AppendTag(tag, fileId);
            }
        }

        public IIndex GetIndex()
        {
            return _index;
        }
        
        private readonly IIndex _index;
        private readonly ITextFilter _textFilter;
        private readonly ITextFilter _tagTaxtFilter;
    }
}
