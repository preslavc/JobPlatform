namespace JobPlatform.Web.Controllers
{
    using JobPlatform.Web.ViewModels.Job;
    using Microsoft.AspNetCore.Mvc;

    public class JobController : BaseController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Details()
        {
            DetailsViewModel dvm = new DetailsViewModel();

            return this.View(dvm);
        }
    }
}
