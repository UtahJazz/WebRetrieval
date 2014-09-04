using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public sealed class SearchResult
    {
        public SearchResult(IList<Snippet> snippets, int resultsCount)
        {
            ResultsCount = resultsCount;
            Snippets = snippets;
        }

        [DataMember]
        public IList<Snippet> Snippets { get; private set; }

        [DataMember]
        public int ResultsCount { get; private set; }
    }
}
