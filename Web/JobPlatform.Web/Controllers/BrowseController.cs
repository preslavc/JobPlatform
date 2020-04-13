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
        private readonly IEmployerService employerService;

        public BrowseController(
            IJobPostsService jobPostsService,
            IEmployerService employerService)
        {
            this.jobPostsService = jobPostsService;
            this.employerService = employerService;
        }

        // [Route("Browse/Jobs/")]
        // [Route("Browse/Jobs/{page}")]
        public IActionResult Jobs([FromQuery]int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            BrowseViewModel viewModel = new BrowseViewModel()
            {
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCount() / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
            };

            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel()
            {
                JobPosts = this.jobPostsService.GetAll<JobPostViewModel>(page),
            };

            return this.View(viewModel);
        }

        public IActionResult Employers([FromQuery]int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            EmployersDisplayViewModel viewModel = new EmployersDisplayViewModel()
            {
                Employers = this.employerService.GetAll<EmployerViewModel>(page),
                PagesCount = (int)Math.Ceiling(this.employerService.GetEmployerCount() / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
            };

            return this.View(viewModel);
        }

        public IActionResult Search([FromQuery]string keyword, string city, int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            SearchViewModel viewModel = new SearchViewModel()
            {
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCountBySearch(keyword, city) / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
                Keyword = keyword,
                City = city,
            };

            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel
            {
                JobPosts = this.jobPostsService.GetAllBy<JobPostViewModel>(keyword, city, page),
            };
            return this.View(viewModel);
        }

        // [Route("Browse/ByTag/")]
        // [Route("Browse/ByTag/{tag}")]
        public IActionResult ByTag(string tag, int? page)
        {
            if (tag == null || tag == string.Empty)
            {
                return this.Redirect("/Browse/Jobs/");
            }

            if (!page.HasValue)
            {
                page = 1;
            }

            tag = tag.ToLower();
            TagIndexViewModel viewModel = new TagIndexViewModel()
            {
                Name = tag,
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCountByTag(tag) / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
            };

            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel
            {
                JobPosts = this.jobPostsService.GetAllByTag<JobPostViewModel>(tag, page),
            };

            return this.View(viewModel);
        }

        // [Route("Browse/ByEmployer/")]
        // [Route("Browse/ByEmployer/{name}")]
        public IActionResult ByEmployer([FromQuery]string name, int? page)
        {
            if (name == null || name == string.Empty)
            {
                return this.Redirect("/Browse/Jobs/");
            }

            if (!page.HasValue)
            {
                page = 1;
            }

            BrowseViewModel viewModel = new BrowseViewModel()
            {
                Name = name,
                PagesCount = (int)Math.Ceiling(this.jobPostsService.GetJobCountByEmployer(name) / GlobalConstants.ItemsPerPage),
                CurrentPage = (int)page,
            };

            viewModel.JobsDisplayViewModel = new JobsDisplayViewModel
            {
                JobPosts = this.jobPostsService.GetAllByEmployer<JobPostViewModel>(name, (int)page),
            };

            return this.View(viewModel);
        }
    }
}
