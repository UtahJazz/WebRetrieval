using System.Collections.Generic;

namespace SearchCore.Ranger.ParametersReducer
{
    public sealed class HardcodedWeightsReducer : SumBasedReducer
    {
        public HardcodedWeightsReducer(
            double tfIdfWeight,
            double wordNearestWeight,
            double tagBasedWeight,
            double titleBasedWeight,
            double voteBasedWeight,
            double userPreferenceWeight)
        {
            _hardcodeDictionary = new Dictionary<string, double>
            {
                {TfIdfKey, tfIdfWeight},
                {WordNearestKey, wordNearestWeight},
                {TagBasedKey, tagBasedWeight},
                {TitleBasedKey, titleBasedWeight},
                {VoteBasedKey, voteBasedWeight},
                {UserPreferenceKey, userPreferenceWeight}
            };
        }

        protected override IDictionary<string, double> GetWeights()
        {
            return _hardcodeDictionary;
        }

        private readonly IDictionary<string, double> _hardcodeDictionary;
    }
}
