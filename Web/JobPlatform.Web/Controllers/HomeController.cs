namespace JobPlatform.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using JobPlatform.Common;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels;
    using JobPlatform.Web.ViewModels.Browse;
    using JobPlatform.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IJobPostsService jobPostsService;

        public HomeController(IJobPostsService jobPostsService)
        {
            this.jobPostsService = jobPostsService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCount() / GlobalConstants.ItemsPerPage),
                CurrentPage = 1,
            };
            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel()
            {
                JobPosts = this.jobPostsService.GetAll<JobPostViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult HttpError(int statusCode)
        {
            return this.View(statusCode);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
