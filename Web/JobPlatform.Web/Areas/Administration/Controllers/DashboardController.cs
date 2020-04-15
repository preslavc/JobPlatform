namespace JobPlatform.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobPostsService jobPostsService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IReportService reportService;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            IJobPostsService jobPostsService,
            IApplicationUserService applicationUserService,
            IReportService reportService)
        {
            this.userManager = userManager;
            this.jobPostsService = jobPostsService;
            this.applicationUserService = applicationUserService;
            this.reportService = reportService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                ActiveJobs = (int)this.jobPostsService.GetJobCount(),
                ActiveUsers = this.applicationUserService.GetUserCount(),
                Reports = this.reportService.GetAllPostReports<ReportViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
