namespace JobPlatform.Web.ViewModels.Shared
{
    using System.Collections.Generic;

    public class JobsDisplayViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<JobPostViewModel> JobPosts { get; set; }
    }
}
