namespace JobPlatform.Services.Data.Tests.Controllers
{
    using JobPlatform.Data.Models;
    using JobPlatform.Web.Controllers;
    using JobPlatform.Web.ViewModels.Reports;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class ReportControllerTests
    {
        [Fact]
        public void ActionCreateShouldReturnCorrectViewModels()
        {
            var jobPostMockService = new Mock<IJobPostsService>();
            jobPostMockService.Setup(
                x => x.GetJobPost(1))
                .Returns(new JobPost() { Id = 1, Title = "TestTitle" });

            var controller = new ReportController(jobPostMockService.Object, null);
            var result = controller.Create(1, "test");
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<ReportViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as ReportViewModel;
            Assert.Equal(1, viewModel.PostId);
            Assert.Equal("TestTitle", viewModel.PostTitle);

            jobPostMockService.Verify(x => x.GetJobPost(1), Times.Once);
        }
    }
}
