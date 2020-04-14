namespace JobPlatform.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            ISettingsService settingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.settingsService = settingsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            //var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            //return this.View(viewModel);
            return Ok();
        }

        //public async Task<IActionResult> DemoteUserRole(string userId, string role)
        //{
        //    var user = await this.userManager.FindByIdAsync(userId);
        //    await this.userManager.RemoveFromRoleAsync(user, role);
        //    return Redirect();
        //}
    }
}
