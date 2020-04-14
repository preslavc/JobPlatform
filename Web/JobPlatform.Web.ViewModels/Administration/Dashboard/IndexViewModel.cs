namespace JobPlatform.Web.ViewModels.Administration.Dashboard
{
    public class IndexViewModel
    {
        //public int SettingsCount { get; set; }

        //public IEnumerable<ActiveJobsViewModel> JobPosts { get; set; }

        public int ActiveJobs { get; set; }

        public int ActiveUsers { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
