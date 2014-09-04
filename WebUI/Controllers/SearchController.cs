using System;
using System.Web;
using System.Web.Mvc;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public sealed class SearchController : Controller
    {
        public SearchController()
        {
            _searchService = new RemoteSearchService.SearchService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSearchResult(
            string query,
            int page,
            bool isCustomRangeEnable)
        {
            var userId = GetUserId();
            var searchResult = _searchService.Search(
                query,
                page,
                true,
                userId.ToString(),
                isCustomRangeEnable,
                true);

            var searchVewModel = new SearchResultViewModel(
                searchResult,
                new SearchZoneViewModel(
                    query,
                    isCustomRangeEnable,
                    page));

            return View("SearchResult", searchVewModel);
        }

        [HttpGet]
        public ActionResult NewSearch(
            string query,
            bool isCustomRangeEnable)
        {
            return GetSearchResult(
                query,
                0,
                isCustomRangeEnable);
        }

        [HttpGet]
        public RedirectResult RegistrateTransition(
            string redirectionUrl,
            int fileId,
            bool isNeedCustomRange)
        {
            var userId = GetUserId();

            if (isNeedCustomRange)
            {
                _searchService.MarkAsLiked(
                    userId.ToString(),
                    fileId,
                    true);    
            }

            return Redirect(redirectionUrl);
        }

        private Guid GetUserId()
        {
            string userIdString;
            if (Request.Cookies[UserIdKey] == null)
            {
                userIdString = Guid.NewGuid().ToString();
                var userIdCookie = new HttpCookie(
                    UserIdKey,
                    userIdString)
                {
                    Expires = DateTime.Now.AddDays(1)
                };

                Response.SetCookie(userIdCookie);
            }
            else
            {
                userIdString = Request.Cookies[UserIdKey].Value;
            }

            return Guid.Parse(userIdString);
        }

        private const string UserIdKey = "UserId";
        private readonly RemoteSearchService.SearchService _searchService;
    }
}
