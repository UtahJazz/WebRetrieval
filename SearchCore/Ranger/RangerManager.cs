using System;
using System.Collections.Generic;
using System.Linq;
using SearchCore.Ranger.ParameterNormalizer;
using SearchCore.Ranger.ParametersReducer;
using SearchCore.Ranger.RangerFilter;

namespace SearchCore.Ranger
{
    public sealed class RangerManager : IRanger
    {
        public RangerManager(
            IRankParameterCalculator parameterCalculator,
            IParametersResucer parametersResucer)
        {
            _parameterCalculator = parameterCalculator;
            _parametersResucer = parametersResucer;
        }

        public IEnumerable<int> Rank(
            Guid userId,
            string userQuery, 
            IEnumerable<int> files)
        {
            var fileParameters = files.AsParallel().Select(file => new RangerParameter(file)).ToArray();
            _parameterCalculator.CalculateParameter(fileParameters, userQuery, userId);
            _normalizer.Normalize(fileParameters);

            _parametersResucer.ReduceRankPrameters(fileParameters);

            return fileParameters.OrderBy(x => x.FullRank).Select(x => x.FileId);
        }

        private readonly ParametersNormalizer _normalizer = new ParametersNormalizer();
        private readonly IRankParameterCalculator _parameterCalculator;
        private readonly IParametersResucer _parametersResucer;
    }
}
