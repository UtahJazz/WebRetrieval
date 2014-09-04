using System;
using System.Linq;
using System.Threading.Tasks;
using SearchCore.Index;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.Ranger.RangerFilter
{
    public sealed class TfIdfParameterCalculator : IRankParameterCalculator
    {
        public TfIdfParameterCalculator(
            IIndex index,
            ITextFilter textFilter)
        {
            _index = index;
            _textFilter = textFilter;
        }

        public void CalculateParameter(
            RangerParameter[] parameters, 
            string userQuery,
            Guid userId)
        {
            if (parameters.Length <= 1)
            {
                return;
            }

            var queryWordsBag = StringUtils.SplitByLowerWord(
                _textFilter.Filter(userQuery)).ToArray();

            Parallel.ForEach(parameters,
                parameter =>
                {
                    parameter.TfIdf = -TfIdfScore(queryWordsBag, parameter.FileId);
                });
        }
        private double TfIdfScore(string[] queryWords, int document)
        {
            return queryWords.Sum(word => WordWeight(word, document));
        }

        private double WordWeight(string word, int document)
        {
            var frequency = _index.GetWordFrequency(word, document) + 1;

            return (1 + Math.Log(frequency)) * WordIdf(word);
        }

        private double WordIdf(string word)
        {
            var docCount = _index.GetDocumentsCountWithWord(word) + 1;

            return Math.Log10(_index.DocumentCount + 1 / (double)docCount);
        }

        private readonly IIndex _index;
        private readonly ITextFilter _textFilter;
    }
}
