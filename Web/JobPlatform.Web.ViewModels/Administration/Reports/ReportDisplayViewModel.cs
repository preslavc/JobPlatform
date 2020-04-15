namespace JobPlatform.Web.ViewModels.Administration.Reports
{
    using System.Collections.Generic;

    using JobPlatform.Web.ViewModels.Administration.Dashboard;

    public class ReportDisplayViewModel
    {
        public IEnumerable<ReportViewModel> Reports { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
