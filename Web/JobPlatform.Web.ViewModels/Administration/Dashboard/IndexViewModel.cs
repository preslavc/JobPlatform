namespace JobPlatform.Web.ViewModels.Administration.Dashboard
{
    using JobPlatform.Web.ViewModels.Administration.Reports;

    public class IndexViewModel
    {
        public ReportDisplayViewModel ReportDisplayViewModel { get; set; }

        public int ActiveJobs { get; set; }

        public int ActiveUsers { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
