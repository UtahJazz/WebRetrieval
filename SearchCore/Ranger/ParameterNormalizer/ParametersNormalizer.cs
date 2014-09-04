using System.Linq;
using SearchCore.Ranger.RangerFilter;

namespace SearchCore.Ranger.ParameterNormalizer
{
    public sealed class ParametersNormalizer
    {
        public void Normalize(RangerParameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                return;
            }

            NormalizeParameter(
                parameters, 
                Rank.TfIdf);

            NormalizeParameter(
                parameters, 
                Rank.WordNearest);

            NormalizeParameter(
                parameters, 
                Rank.TitleBased);

            NormalizeParameter(
                parameters, 
                Rank.VoteBased);

            NormalizeParameter(
                parameters, 
                Rank.TagBased);

            NormalizeParameter(
                parameters, 
                Rank.TagBased);
        }

        private void NormalizeParameter(
            RangerParameter[] parameters, 
            Rank rank)
        {
            var minValue = parameters.Min(paramter => paramter.GetRank(rank));

            foreach (var rangerParameter in parameters)
            {
                rangerParameter.SetRank(rank, 1 + rangerParameter.GetRank(rank)-minValue);
            }

            var maxValue = parameters.Max(paramter => paramter.GetRank(rank)) + 1;

            foreach (var rangerParameter in parameters)
            {
                rangerParameter.SetRank(rank, rangerParameter.GetRank(rank) / maxValue);
            }
        }
    }
}
