namespace WebUI.ViewModel
{
    public sealed class SearchZoneViewModel
    {
        public SearchZoneViewModel(
            string userQuery, 
            bool isCustomRangeEnable,
            int pageNumber)
        {
            PageNumber = pageNumber;
            IsCustomRangeEnable = isCustomRangeEnable;
            UserQuery = userQuery;
        }

        public string UserQuery { get; private set; }

        public bool IsCustomRangeEnable { get; private set; }

        public int PageNumber { get; private set; }
    }
}