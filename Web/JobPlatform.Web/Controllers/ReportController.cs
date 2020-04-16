namespace JobPlatform.Web.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Reports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReportController : BaseController
    {
        private readonly IJobPostsService jobPostsService;
        private readonly IReportService reportService;

        public ReportController(
            IJobPostsService jobPostsService,
            IReportService reportService)
        {
            this.jobPostsService = jobPostsService;
            this.reportService = reportService;
        }

        [Authorize]
        public IActionResult Create(int postId, string slug)
        {
            ReportViewModel viewModel = new ReportViewModel();
            JobPost jobPost = this.jobPostsService.GetJobPost(postId);
            if (jobPost == null)
            {
                return this.NotFound();
            }

            viewModel.PostId = jobPost.Id;
            viewModel.PostTitle = jobPost.Title;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReportViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.reportService.CreateAsync(
                viewModel.Title,
                viewModel.Message,
                viewModel.PostId,
                null);
            this.TempData["InfoMessage"] = "Report created successfully!";
            return this.Redirect($"/Job/{viewModel.PostId}/");
        }
    }
}
