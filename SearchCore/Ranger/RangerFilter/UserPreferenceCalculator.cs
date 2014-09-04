using System;
using System.Linq;
using System.Threading.Tasks;
using SearchCore.Metadata;
using SearchCore.UserStatistics;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class UserPreferenceCalculator : IRankParameterCalculator
    {
        public UserPreferenceCalculator(IUserStatisticsLoader userStatisticsLoader, IMetadataPool metadataPool)
        {
            _userStatisticsLoader = userStatisticsLoader;
            _metadataPool = metadataPool;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery, 
            Guid userId)
        {
            var userStatistics = _userStatisticsLoader.Load(userId);

            Parallel.ForEach(parameters,
                rangerParameter =>
                {
                    rangerParameter.UserPreferenceBased =
                        - CalculateSinglePreference(
                            rangerParameter.FileId,
                            userStatistics);
                });
        }

        private int CalculateSinglePreference(int fileId, IUserStatistics userStatistics)
        {
            var fileMetadata = _metadataPool.GetMetadata(fileId);

            return fileMetadata.GetTags().Sum(tag => userStatistics.GetTagPreference(tag));
        }


        private readonly IUserStatisticsLoader _userStatisticsLoader;
        private readonly IMetadataPool _metadataPool;
    }
}
