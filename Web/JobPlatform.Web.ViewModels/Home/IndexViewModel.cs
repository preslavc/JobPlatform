namespace JobPlatform.Web.ViewModels.Home
{
    using JobPlatform.Web.ViewModels.Browse;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.SearchPartialViewModel = new SearchPartialViewModel();
        }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public SearchPartialViewModel SearchPartialViewModel { get; set; }

        public JobsDisplayViewModel JobsDisplayViewModel { get; set; }
    }
}
