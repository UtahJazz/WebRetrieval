using Common;

namespace WebUI.ViewModel
{
    public sealed class SearchResultViewModel
    {
        public SearchResultViewModel(
            RemoteSearchService.SearchResult searchResult,
            SearchZoneViewModel searchZoneParams)
        {
            SearchResult = searchResult;
            SearchZoneParams = searchZoneParams;
        }

        public RemoteSearchService.SearchResult SearchResult { get; private set; }

        public SearchZoneViewModel SearchZoneParams { get; private set; }

        public int PagesCount
        {
            get
            {
                return
                    SearchResult.ResultsCount/SearchConstants.ResultPerPage +
                    (SearchResult.ResultsCount%SearchConstants.ResultPerPage == 0 ? 0 : 1);
            }
        }
    }
}