using System;
using SearchCore.Metadata;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class VoteBasedRankCalculator : IRankParameterCalculator
    {
        public VoteBasedRankCalculator(IMetadataPool metadataPool)
        {
            _metadataPool = metadataPool;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId)
        {
            foreach (var rangerParameter in parameters)
            {
                rangerParameter.VoteBased = -GetTotalVote(rangerParameter.FileId);
            }
        }

        private int GetTotalVote(int fileId)
        {
            return _metadataPool.GetMetadata(fileId).GetTotalVote();
        }

        private readonly IMetadataPool _metadataPool;
    }
}
