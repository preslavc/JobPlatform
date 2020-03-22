namespace JobPlatform.Web.Controllers
{
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Employers;
    using Microsoft.AspNetCore.Mvc;

    public class EmployerController : BaseController
    {
        private readonly IEmployerService employerService;

        public EmployerController(IEmployerService employerService)
        {
            this.employerService = employerService;
        }

        public IActionResult Id(int id)
        {
            EmployerDetailsViewModel viewModel = this.employerService.GetById<EmployerDetailsViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
