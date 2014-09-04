using System.Collections.Generic;
using System.Linq;
using SearchCore.Index;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.UserQueryProcesser
{
    public sealed class BinaryQueryProcesser : IUserQueryProcesser
    {
        public BinaryQueryProcesser(
            IIndex reverseIndex,
            ITextFilter punctuationFilter,
            ITextFilter tagPunctuationFilter)
        {
            _reverseIndex = reverseIndex;
            _punctuationFilter = punctuationFilter;
            _tagPunctuationFilter = tagPunctuationFilter;
        }

        public IEnumerable<int> GetMatchedFiles(string userQuery)
        {
            var tagWords = SplitAndFilter(userQuery, _tagPunctuationFilter).ToArray();

            if (tagWords.Length == 0)
            {
                return new int[0];
            }

            var result = GetWordsByTag(tagWords[0]);

            for (var i = 1; i < tagWords.Length; i++)
            {
                result = BinaryQueryOperators.And(result.ToArray(), GetWordsByTag(tagWords[i]));
            }

            return result;
        }

        private int[] GetWordsByTag(string tag)
        {
            var tagFiles = _reverseIndex.GetFilesWithTag(tag);
            var wordsBag = SplitAndFilter(tag, _punctuationFilter).ToList();

            if (wordsBag.Count == 0)
            {
                return tagFiles.ToArray();
            }

            var wordFiles = _reverseIndex.GetFilesWithWord(wordsBag[0]).ToArray();
            wordsBag.RemoveAt(0);

            if (wordFiles.Length != 0)
            {
                wordFiles =
                    wordsBag
                        .Aggregate(
                            wordFiles,
                            (current, word) =>
                                BinaryQueryOperators.And(
                                    current,
                                    _reverseIndex.GetFilesWithWord(word).ToArray()));
            }

            return BinaryQueryOperators.Or(tagFiles.ToArray(), wordFiles);
        } 

        private IEnumerable<string> SplitAndFilter(string text, ITextFilter filter)
        {
            return StringUtils.SplitByLowerWord(filter.Filter(text));
        }

        private readonly IIndex _reverseIndex;
        private readonly ITextFilter _punctuationFilter;
        private readonly ITextFilter _tagPunctuationFilter;
    }
}
