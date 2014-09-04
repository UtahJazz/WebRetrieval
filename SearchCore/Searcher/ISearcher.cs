using System;
using Common;

namespace SearchCore.Searcher
{
    public interface ISearcher
    {
        SearchResult Search(
            string userQuery, 
            int page,
            Guid userId, 
            bool isNeedCustomRange);

        void MarkAsLiked(Guid userId, int fileId);
    }
}
