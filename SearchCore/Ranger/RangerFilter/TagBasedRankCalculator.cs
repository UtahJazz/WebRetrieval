using System;
using System.Linq;
using System.Threading.Tasks;
using SearchCore.Metadata;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class TagBasedRankCalculator : IRankParameterCalculator
    {
        public TagBasedRankCalculator(
            IMetadataPool metadataPool,
            ITextFilter tagFilter)
        {
            _metadataPool = metadataPool;
            _tagFilter = tagFilter;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId)
        {
            var queryTags = StringUtils.SplitByLowerWord(
                _tagFilter.Filter(userQuery)).ToArray();

            Parallel.ForEach(parameters,
                rangerParameter =>
                {
                    rangerParameter.TagBased =
                        - TagCountFromQuery(
                            rangerParameter.FileId,
                            queryTags);
                });
        }

        private int TagCountFromQuery(int fileId, string[] queryTags)
        {
            var fileMetadata = _metadataPool.GetMetadata(fileId);

            return queryTags.Count(tag => fileMetadata.GetTags().Contains(tag));
        }

        private readonly IMetadataPool _metadataPool;
        private readonly ITextFilter _tagFilter;
    }
}
