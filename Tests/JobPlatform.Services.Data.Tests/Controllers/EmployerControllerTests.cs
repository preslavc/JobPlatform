namespace JobPlatform.Services.Data.Tests.Controllers
{
    using JobPlatform.Web.Controllers;
    using JobPlatform.Web.ViewModels.Employers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class EmployerControllerTests
    {
        [Fact]
        public void ActionIdShouldReturnCorrectViewModels()
        {
            var employerMockService = new Mock<IEmployerService>();
            employerMockService.Setup(
                x => x.GetById<EmployerDetailsViewModel>(1))
                .Returns(new EmployerDetailsViewModel() { Name = "TestName" });

            var controller = new EmployerController(employerMockService.Object);
            var result = controller.Id(1, "test");
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<EmployerDetailsViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as EmployerDetailsViewModel;
            Assert.Equal("TestName", viewModel.Name);

            employerMockService.Verify(x => x.GetById<EmployerDetailsViewModel>(1), Times.Once);
        }
    }
}
