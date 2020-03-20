namespace JobPlatform.Web.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.Attributes;
    using JobPlatform.Web.ViewModels.Job;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class JobController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobPostsService jobPostsService;

        public JobController(
            UserManager<ApplicationUser> userManager,
            IJobPostsService jobPostsService)
        {
            this.userManager = userManager;
            this.jobPostsService = jobPostsService;
        }

        [AuthorizeRoles(Common.GlobalConstants.AdministratorRoleName, Common.GlobalConstants.EmployerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AuthorizeRoles(Common.GlobalConstants.AdministratorRoleName, Common.GlobalConstants.EmployerRoleName)]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createViewModel);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            int jobPostId = await this.jobPostsService.CreateAsync(
                createViewModel.Title,
                createViewModel.Description,
                createViewModel.City,
                createViewModel.Country,
                (int)user.EmployerId);
            return this.RedirectToAction(nameof(this.Details), new { id = jobPostId });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            DetailsViewModel viewModel = this.jobPostsService.GetById<DetailsViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
