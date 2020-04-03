namespace JobPlatform.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using JobPlatform.Web.ViewModels.Shared;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.SearchPartialViewModel = new SearchPartialViewModel();
        }

        public SearchPartialViewModel SearchPartialViewModel { get; set; }

        public JobsDisplayViewModel JobsDisplayViewModel { get; set; }
    }
}
