namespace JobPlatform.Web.ViewModels.Browse
{
    public class BrowseViewModel
    {
        public BrowseViewModel()
        {
            this.SearchPartialViewModel = new SearchPartialViewModel();
        }

        public string Name { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public SearchPartialViewModel SearchPartialViewModel { get; set; }

        public JobsDisplayViewModel JobsDisplayViewModel { get; set; }
    }
}
