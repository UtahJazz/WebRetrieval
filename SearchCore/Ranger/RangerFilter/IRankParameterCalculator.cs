using System;

namespace SearchCore.Ranger.RangerFilter
{
    public interface IRankParameterCalculator
    {
        void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId);
    }
}
