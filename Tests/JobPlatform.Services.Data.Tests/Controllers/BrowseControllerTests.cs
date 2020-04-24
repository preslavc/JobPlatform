namespace JobPlatform.Services.Data.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using JobPlatform.Web.Controllers;
    using JobPlatform.Web.ViewModels.Browse;
    using JobPlatform.Web.ViewModels.Tags;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class BrowseControllerTests
    {
        [Fact]
        public void ActionJobShouldReturnCorrectViewModels()
        {
            var jobPostMockService = new Mock<IJobPostsService>();
            jobPostMockService.Setup(
                x => x.GetAll<JobPostViewModel>(1))
                .Returns(new List<JobPostViewModel>
                {
                    new JobPostViewModel(),
                    new JobPostViewModel(),
                });

            var controller = new BrowseController(jobPostMockService.Object, null);
            var result = controller.Jobs(1);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<BrowseViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as BrowseViewModel;
            Assert.IsType<JobsDisplayViewModel>(viewModel.JobsDisplayViewModel);
            Assert.IsType<SearchPartialViewModel>(viewModel.SearchPartialViewModel);
            Assert.Equal(2, viewModel.JobsDisplayViewModel.JobPosts.Count());

            jobPostMockService.Verify(x => x.GetAll<JobPostViewModel>(1), Times.Once);
        }

        [Fact]
        public void ActionEmployerShouldReturnCorrectViewModels()
        {
            var employerMockService = new Mock<IEmployerService>();
            employerMockService.Setup(
                x => x.GetAll<EmployerViewModel>(1))
                .Returns(new List<EmployerViewModel>
                {
                    new EmployerViewModel(),
                    new EmployerViewModel(),
                });

            var controller = new BrowseController(null, employerMockService.Object);
            var result = controller.Employers(1);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<EmployersDisplayViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as EmployersDisplayViewModel;
            Assert.Equal(2, viewModel.Employers.Count());

            employerMockService.Verify(x => x.GetAll<EmployerViewModel>(1), Times.Once);
        }

        [Fact]
        public void ActionSearchShouldReturnCorrectViewModels()
        {
            var jobPostMockService = new Mock<IJobPostsService>();
            jobPostMockService.Setup(
                x => x.GetAllBy<JobPostViewModel>("key", "city", 1))
                .Returns(new List<JobPostViewModel>
                {
                    new JobPostViewModel(),
                    new JobPostViewModel(),
                });

            var controller = new BrowseController(jobPostMockService.Object, null);
            var result = controller.Search("key", "city", 1);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<SearchViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as SearchViewModel;
            Assert.IsType<JobsDisplayViewModel>(viewModel.JobsDisplayViewModel);
            Assert.IsType<SearchPartialViewModel>(viewModel.SearchPartialViewModel);
            Assert.Equal(2, viewModel.JobsDisplayViewModel.JobPosts.Count());

            jobPostMockService.Verify(x => x.GetAllBy<JobPostViewModel>("key", "city", 1), Times.Once);
        }

        [Fact]
        public void ActioTagShouldReturnCorrectViewModels()
        {
            var jobPostMockService = new Mock<IJobPostsService>();
            jobPostMockService.Setup(
                x => x.GetAllByTag<JobPostViewModel>("tag", 1))
                .Returns(new List<JobPostViewModel>
                {
                    new JobPostViewModel(),
                    new JobPostViewModel(),
                });

            var controller = new BrowseController(jobPostMockService.Object, null);
            var result = controller.ByTag("tag", 1);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<TagIndexViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as TagIndexViewModel;
            Assert.IsType<JobsDisplayViewModel>(viewModel.JobsDisplayViewModel);
            Assert.Equal(2, viewModel.JobsDisplayViewModel.JobPosts.Count());

            jobPostMockService.Verify(x => x.GetAllByTag<JobPostViewModel>("tag", 1), Times.Once);
        }

        [Fact]
        public void ActioByEmployerShouldReturnCorrectViewModels()
        {
            var jobPostMockService = new Mock<IJobPostsService>();
            jobPostMockService.Setup(
                x => x.GetAllByEmployer<JobPostViewModel>("employer", 1))
                .Returns(new List<JobPostViewModel>
                {
                    new JobPostViewModel(),
                    new JobPostViewModel(),
                });

            var controller = new BrowseController(jobPostMockService.Object, null);
            var result = controller.ByEmployer("employer", 1);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<BrowseViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as BrowseViewModel;
            Assert.IsType<JobsDisplayViewModel>(viewModel.JobsDisplayViewModel);
            Assert.Equal(2, viewModel.JobsDisplayViewModel.JobPosts.Count());

            jobPostMockService.Verify(x => x.GetAllByEmployer<JobPostViewModel>("employer", 1), Times.Once);
        }
    }
}
