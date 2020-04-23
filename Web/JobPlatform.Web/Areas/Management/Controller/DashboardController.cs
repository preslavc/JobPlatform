namespace JobPlatform.Web.Areas.Management.Controller
{
    using System;
    using System.Threading.Tasks;

    using JobPlatform.Common;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Management.Dashboard;
    using JobPlatform.Web.ViewModels.Management.Jobs;
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

        public async Task<IActionResult> Index(int? page)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            Employer employer = this.employerService.GetById((int)user.EmployerId);
            if (!page.HasValue)
            {
                page = 1;
            }

            IndexViewModel viewModel = new IndexViewModel
            {
                Name = employer.Name,
                ActiveJobs = (int)this.jobPostsService.GetJobCountByEmployer(employer.Name),
                JobPosts = this.jobPostsService.GetAllByEmployer<ActiveJobsViewModel>(employer.Name, page),
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCountByEmployer(employer.Name) / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Jobs(int? page)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            Employer employer = this.employerService.GetById((int)user.EmployerId);

            if (!page.HasValue)
            {
                page = 1;
            }

            JobsViewModel viewModel = new JobsViewModel
            {
                JobPosts = this.jobPostsService.GetAllByEmployer<ActiveJobsViewModel>(employer.Name, page),
                CurrentPage = (int)page,
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCountByEmployer(employer.Name) / GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> MessagesByPost(int postId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            JobApplyViewModel viewModel = this.jobPostsService.GetById<JobApplyViewModel>(postId);
            if (viewModel == null || viewModel.EmployerId != user.EmployerId)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Messages(int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            MessageBrowseViewModel viewModel = new MessageBrowseViewModel
            {
                CvMessages = this.cvmessageService.GetAllMessagesAsync<CvMessagesViewModel>(user.Id, page),
                CurrentPage = (int)page,
                PagesCount = (int)Math.Ceiling(this.cvmessageService.GetMessageCount(user.Id) / GlobalConstants.ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            CvMessage cvmessage = this.cvmessageService.GetMessage(id);

            if (cvmessage == null)
            {
                return this.NotFound();
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            JobApplyViewModel viewModel = this.jobPostsService.GetById<JobApplyViewModel>(cvmessage.JobPostId);
            if (viewModel == null || viewModel.EmployerId != user.EmployerId)
            {
                return this.NotFound();
            }

            await this.cvmessageService.DeleteAsync(cvmessage);
            return this.Redirect($"/Management/Dashboard/Messages?postId={cvmessage.JobPostId}");
        }

        public async Task<IActionResult> MessageId(int messageId)
        {
            CvMessagesViewModel viewModel = this.cvmessageService.GetById<CvMessagesViewModel>(messageId);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (viewModel == null || viewModel.JobPost.EmployerId != user.EmployerId)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
