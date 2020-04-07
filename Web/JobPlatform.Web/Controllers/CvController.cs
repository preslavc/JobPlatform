namespace JobPlatform.Web.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.CvMessages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CvController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICvMessageService cvmessageService;
        private readonly IJobPostsService jobPostsService;

        public CvController(
            UserManager<ApplicationUser> userManager,
            ICvMessageService cvmessageService,
            IJobPostsService jobPostsService)
        {
            this.userManager = userManager;
            this.cvmessageService = cvmessageService;
            this.jobPostsService = jobPostsService;
        }

        [Authorize]
        public async Task<IActionResult> Apply(int postId)
        {
            if (!this.jobPostsService.JobPostExist(postId))
            {
                return this.NotFound();
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CreateCvViewModel viewModel = new CreateCvViewModel
            {
                PostId = postId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Apply(CreateCvViewModel createCvViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createCvViewModel);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.cvmessageService.SendCvAsync(
                user,
                createCvViewModel.PostId,
                createCvViewModel.Message,
                createCvViewModel.CvFile,
                createCvViewModel.FirstName,
                createCvViewModel.LastName);
            return this.Redirect("/Browse/Jobs/");
        }
    }
}
