using System;
using System.Threading.Tasks;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class ParameterCalculatorsAggregator : IRankParameterCalculator
    {
        public ParameterCalculatorsAggregator(IRankParameterCalculator[] calculators)
        {
            _calculators = calculators;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId)
        {
            Parallel.ForEach(_calculators,
                rankParameterCalculator => rankParameterCalculator.CalculateParameter(
                    parameters,
                    userQuery,
                    userId));
        }

        private readonly IRankParameterCalculator[] _calculators;
    }
}
