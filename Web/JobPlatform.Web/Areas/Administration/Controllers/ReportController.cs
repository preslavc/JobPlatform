namespace JobPlatform.Web.Areas.Administration.Controllers
{
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class ReportController : AdministrationController
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult Id(int id)
        {
            ReportViewModel viewModel = this.reportService.GetById<ReportViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
