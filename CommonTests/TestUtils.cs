using System;
using SearchCore.Ranger;
using SearchCore.Ranger.ParametersReducer;
using SearchCore.Ranger.RangerFilter;

namespace CommonTests
{
    public static class TestUtils
    {
        public static IRanger CreateSingleParameterRager(
            IRankParameterCalculator parameterCalculator)
        {
            return new RangerManager(
                new ParameterCalculatorsAggregator(
                    new []
                    {
                        parameterCalculator
                    }),
                new HardcodedWeightsReducer(
                    tfIdfWeight: 1.0,
                    wordNearestWeight: 1.0,
                    tagBasedWeight: 1.0,
                    titleBasedWeight: 1.0,
                    voteBasedWeight: 1.0,
                    userPreferenceWeight: 1.0));
        }

        public static Guid EmptyUserGuid = new Guid();
    }
}
