namespace JobPlatform.Web.ViewModels.Management.Jobs
{
    using System.Collections.Generic;

    public class JobsViewModel
    {
        public IEnumerable<ActiveJobsViewModel> JobPosts { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
