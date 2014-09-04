using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchCore.Ranger.ParametersReducer
{
    public sealed class FileBasedWeightsReducer : SumBasedReducer
    {
        public FileBasedWeightsReducer(string weightsFilePath)
        {
            _weightsFilePath = weightsFilePath;
        }

        protected override IDictionary<string, double> GetWeights()
        {
            var weightLines = File.ReadAllLines(_weightsFilePath);

            return weightLines
                .Select(weightLine => weightLine.Split(' '))
                .ToDictionary(
                    weightPices => weightPices[0], 
                    weightPices => 
                        double.Parse(weightPices[1]));
        }

        private readonly string _weightsFilePath;
    }
}
