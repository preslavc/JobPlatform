namespace JobPlatform.Web.Areas.Management.Controller
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Management.Dashboard;
    using JobPlatform.Web.ViewModels.Management.Messages;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : ManagementController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobPostsService jobPostsService;
        private readonly IEmployerService employerService;
        private readonly ICvMessageService cvmessageService;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            IJobPostsService jobPostsService,
            IEmployerService employerService,
            ICvMessageService cvmessageService)
        {
            this.userManager = userManager;
            this.jobPostsService = jobPostsService;
            this.employerService = employerService;
            this.cvmessageService = cvmessageService;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            Employer employer = this.employerService.GetById((int)user.EmployerId);
            IndexViewModel viewModel = new IndexViewModel
            {
                Name = employer.Name,
                JobPosts = this.jobPostsService.GetAllByEmployer<ActiveJobsViewModel>(employer.Name),
            };
            return this.View(viewModel);
        }

        //[Route("Management/Messages/")]
        //[Route("Management/Messages/{postId}")]
        public async Task<IActionResult> Messages([FromQuery]int postId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            JobApplyViewModel viewModel = this.jobPostsService.GetById<JobApplyViewModel>(postId);
            if (viewModel == null || viewModel.EmployerId != user.EmployerId)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
