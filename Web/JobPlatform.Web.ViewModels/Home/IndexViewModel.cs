﻿namespace JobPlatform.Web.ViewModels.Home
{
    using JobPlatform.Web.ViewModels.Browse;

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
