using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchCore.Ranger.RangerFilter;

namespace SearchCore.Ranger.ParametersReducer
{
    public abstract class SumBasedReducer : IParametersResucer
    {
        public void ReduceRankPrameters(RangerParameter[] parameters)
        {
            var weights = GetWeights();
            Console.WriteLine("Query range ranks");

            Parallel.ForEach(parameters,
                rangerParameter =>
                {
                    rangerParameter.FullRank =
                        rangerParameter.TfIdf*weights[TfIdfKey] +
                        rangerParameter.WordNearest*weights[WordNearestKey] +
                        rangerParameter.TagBased*weights[TagBasedKey] +
                        rangerParameter.TitleBased*weights[TitleBasedKey] +
                        rangerParameter.VoteBased*weights[VoteBasedKey] +
                        rangerParameter.UserPreferenceBased*weights[UserPreferenceKey];

                    // LogParameterToConsole(rangerParameter, weights);
                });
            
            

            Console.WriteLine("\n\n");
        }

        private static void LogParameterToConsole(
            RangerParameter param,
            IDictionary<string, double> weights)
        {
            Console.Write("TfIdf: ");
            Console.WriteLine(param.TfIdf * weights[TfIdfKey]);
            Console.Write("WordNearest: ");
            Console.WriteLine(param.WordNearest * weights[WordNearestKey]);
            Console.Write("TagBased: ");
            Console.WriteLine(param.TagBased * weights[TagBasedKey]);
            Console.Write("TagBased: ");
            Console.WriteLine(param.TitleBased * weights[TitleBasedKey]);
            Console.Write("VoteBased: ");
            Console.WriteLine(param.VoteBased * weights[VoteBasedKey]);
            Console.Write("UserPreferenceBased: ");
            Console.WriteLine(param.UserPreferenceBased * weights[UserPreferenceKey]);
        }

        protected abstract IDictionary<string, double> GetWeights();

        protected const string TfIdfKey = "TfIdf";
        protected const string WordNearestKey = "WordNearest";
        protected const string TagBasedKey = "TagBased";
        protected const string TitleBasedKey = "TitleBased";
        protected const string VoteBasedKey = "VoteBased";
        protected const string UserPreferenceKey = "UserPreference";
    }
}
