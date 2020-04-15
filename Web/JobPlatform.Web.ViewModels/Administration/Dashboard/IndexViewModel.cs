namespace JobPlatform.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class IndexViewModel
    {

        public IEnumerable<ReportViewModel> Reports { get; set; }

        public int ActiveJobs { get; set; }

        public int ActiveUsers { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
