namespace JobPlatform.Web.Controllers
{
    using System;

    using JobPlatform.Common;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Browse;
    using JobPlatform.Web.ViewModels.Tags;
    using Microsoft.AspNetCore.Mvc;

    public class BrowseController : BaseController
    {
        private readonly IJobPostsService jobPostsService;

        public BrowseController(IJobPostsService jobPostsService)
        {
            this.jobPostsService = jobPostsService;
        }

        // [Route("Browse/Jobs/")]
        // [Route("Browse/Jobs/{page}")]
        public IActionResult Jobs([FromQuery]int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            BrowseViewModel viewModel = new BrowseViewModel();
            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel()
            {
                JobPosts = this.jobPostsService.GetAll<JobPostViewModel>(page),
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCount() / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
            };

            return this.View(viewModel);
        }

        public IActionResult Search([FromQuery]string keyword, string city)
        {
            BrowseViewModel viewModel = new BrowseViewModel();
            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel
            {
                JobPosts = this.jobPostsService.GetAllBy<JobPostViewModel>(keyword, city),
            };
            return this.View(viewModel);
        }

        // [Route("Browse/ByTag/")]
        // [Route("Browse/ByTag/{tag}")]
        public IActionResult ByTag(string tag)
        {
            if (tag == null || tag == string.Empty)
            {
                return this.Redirect("/Browse/Jobs/");
            }

            tag = tag.ToLower();
            TagIndexViewModel viewModel = new TagIndexViewModel()
            {
                Name = tag,
            };

            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel
            {
                JobPosts = this.jobPostsService.GetAllByTag<JobPostViewModel>(tag),
            };

            return this.View(viewModel);
        }

        // [Route("Browse/ByEmployer/")]
        // [Route("Browse/ByEmployer/{name}")]
        public IActionResult ByEmployer([FromQuery]string name)
        {
            if (name == null || name == string.Empty)
            {
                return this.Redirect("/Browse/Jobs/");
            }

            BrowseViewModel viewModel = new BrowseViewModel()
            {
                Name = name,
            };

            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel
            {
                JobPosts = this.jobPostsService.GetAllByEmployer<JobPostViewModel>(name),
            };

            return this.View(viewModel);
        }
    }
}
