using SearchCore.Ranger.RangerFilter;

namespace SearchCore.Ranger.ParametersReducer
{
    public interface IParametersResucer
    {
        void ReduceRankPrameters(RangerParameter[] parameters);
    }
}
