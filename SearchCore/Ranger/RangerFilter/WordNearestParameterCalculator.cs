using System;
using System.Linq;
using SearchCore.Metadata;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.Ranger.RangerFilter
{
    public class WordNearestParameterCalculator : IRankParameterCalculator
    {
        public WordNearestParameterCalculator(
            IMetadataPool metadataPool,
            ITextFilter wordFilter)
        {
            _metadataPool = metadataPool;
            _wordFilter = wordFilter;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId)
        {
            if (parameters.Length <= 1)
            {
                return;
            }

            var queryWordsBag = StringUtils.SplitByLowerWord(
                _wordFilter.Filter(userQuery)).ToArray();

            foreach (var parameter in parameters)
            {
                parameter.WordNearest = VectorUtils.MinimalDistance(
                    queryWordsBag.Select(
                        word =>
                            _metadataPool
                                .GetMetadata(parameter.FileId)
                                .GetWordPositions(word)
                                .Select(WordPositionToInt)
                                .OrderBy(x => x)
                                .ToArray())
                        .Select(InTagWordsFilter)
                        .ToArray());
            }
        }

        private static int[] InTagWordsFilter(int[] vector)
        {
            return vector.Length == 0 ? new[] { TagCost } : vector;
        }

        private static int WordPositionToInt(WordPosition wordPosition)
        {
            return wordPosition.Paragraph * ParagraphCost +
                   wordPosition.InParagraphPosition;
        }

        private readonly IMetadataPool _metadataPool;
        private readonly ITextFilter _wordFilter;

        private const int ParagraphCost = 20;
        private const int TagCost = -5;
    }
}
