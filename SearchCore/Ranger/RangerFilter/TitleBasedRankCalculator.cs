using System;
using System.Linq;
using System.Threading.Tasks;
using SearchCore.Metadata;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class TitleBasedRankCalculator : IRankParameterCalculator
    {
        public TitleBasedRankCalculator(
            IMetadataPool metadataPool,
            ITextFilter wordTextFilter)
        {
            _metadataPool = metadataPool;
            _wordTextFilter = wordTextFilter;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId)
        {
            var queryWordBag = StringUtils.SplitByLowerWord(
                _wordTextFilter.Filter(userQuery)).ToArray();

            Parallel.ForEach(parameters,
                rangerParameter =>
                {
                    rangerParameter.TitleBased = InTitleWordsCount(rangerParameter.FileId, queryWordBag);
                });
        }

        private int InTitleWordsCount(int fileId, string[] queryWordBag)
        {
            var fileMetadata = _metadataPool.GetMetadata(fileId);
            return queryWordBag.Sum(
                    word => 
                        fileMetadata
                            .GetWordPositions(word)
                            .Any(
                                x => 
                                    x.InParagraphPosition == 0) ? 1 : 0);
        }

        private readonly IMetadataPool _metadataPool;
        private readonly ITextFilter _wordTextFilter;
    }
}
