namespace JobPlatform.Services.Data.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using JobPlatform.Web.Controllers;
    using JobPlatform.Web.ViewModels.Browse;
    using JobPlatform.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void ActionIndexShouldReturnCorrectViewModels()
        {
            var jobPostMockService = new Mock<IJobPostsService>();
            jobPostMockService.Setup(
                x => x.GetAll<JobPostViewModel>(null))
                .Returns(new List<JobPostViewModel>
                {
                    new JobPostViewModel(),
                    new JobPostViewModel(),
                });

            var controller = new HomeController(jobPostMockService.Object);
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<IndexViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as IndexViewModel;
            Assert.IsType<JobsDisplayViewModel>(viewModel.JobsDisplayViewModel);
            Assert.IsType<SearchPartialViewModel>(viewModel.SearchPartialViewModel);
            Assert.Equal(2, viewModel.JobsDisplayViewModel.JobPosts.Count());

            jobPostMockService.Verify(x => x.GetAll<JobPostViewModel>(null), Times.Once);
        }
    }
}
