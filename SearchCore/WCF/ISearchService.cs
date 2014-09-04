using System;
using System.ServiceModel;
using Common;

namespace SearchCore.WCF
{
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        SearchResult Search(
            string userQuery, 
            int page,
            Guid userId,
            bool isNeedCustomRange);

        [OperationContract]
        void MarkAsLiked(
            Guid userId,
            int fileId);
    }
}
