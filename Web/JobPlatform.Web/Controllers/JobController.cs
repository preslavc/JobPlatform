namespace JobPlatform.Web.Controllers
{
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Job;
    using Microsoft.AspNetCore.Mvc;

    public class JobController : BaseController
    {
        private readonly IJobPostsService jobPostsService;

        public JobController(IJobPostsService jobPostsService)
        {
            this.jobPostsService = jobPostsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

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
