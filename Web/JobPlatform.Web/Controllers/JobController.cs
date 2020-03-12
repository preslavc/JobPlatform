namespace JobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class JobController : BaseController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Details(int jobId)
        {
            return this.View();
        }
    }
}
