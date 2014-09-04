using System;
using System.ServiceModel;
using Common;
using SearchCore.Searcher;

namespace SearchCore.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public sealed class SearchService : ISearchService
    {
        public SearchService(ISearcher searcher)
        {
            _searcher = searcher;
        }

        public SearchResult Search(string userQuery, int page, Guid userId, bool isNeedCustomRange)
        {
            return _searcher.Search(
                userQuery, 
                page,
                userId,
                isNeedCustomRange);
        }

        public void MarkAsLiked(Guid userId, int fileId)
        {
            _searcher.MarkAsLiked(userId, fileId);
        }

        private readonly ISearcher _searcher;
    }
}
