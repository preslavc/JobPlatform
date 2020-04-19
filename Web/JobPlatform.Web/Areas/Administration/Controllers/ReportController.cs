namespace JobPlatform.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Administration.Reports;
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

        [HttpPost]
        public async Task<IActionResult> Update(ReportViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel = this.reportService.GetById<ReportViewModel>(viewModel.Id);
                if (viewModel == null)
                {
                    return this.NotFound();
                }
                return this.View(viewModel);
            }

            await this.reportService.UpdateAsync(viewModel.Id, viewModel.Resolved, viewModel.ResolvedInfo);
            this.TempData["InfoMessage"] = "Report updated successfully!";
            return this.Redirect("/Administration/Dashboard/Reports");
        }
    }
}
