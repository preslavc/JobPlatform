﻿namespace JobPlatform.Web.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.Infrastructure.Attributes;
    using JobPlatform.Web.ViewModels.Jobs;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class JobController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobPostsService jobPostsService;

        public JobController(
            UserManager<ApplicationUser> userManager,
            IJobPostsService jobPostsService)
        {
            this.userManager = userManager;
            this.jobPostsService = jobPostsService;
        }

        [AuthorizeRoles(Common.GlobalConstants.AdministratorRoleName, Common.GlobalConstants.EmployerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AuthorizeRoles(Common.GlobalConstants.AdministratorRoleName, Common.GlobalConstants.EmployerRoleName)]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createViewModel);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            int jobPostId = await this.jobPostsService.CreateAsync(
                createViewModel.Title,
                createViewModel.Description,
                createViewModel.City,
                createViewModel.Country,
                (int)user.EmployerId);
            return this.RedirectToAction(nameof(this.Id), new { id = jobPostId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            JobPost jobPost = this.jobPostsService.GetJobPost(id);
            if (jobPost == null)
            {
                return this.NotFound();
            }

            if (!await this.UserPermission(jobPost.EmployerId))
            {
                return this.Unauthorized();
            }

            await this.jobPostsService.DeleteAsync(jobPost);
            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            EditViewModel viewModel = this.jobPostsService.GetById<EditViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            if (!await this.UserPermission(viewModel.EmployerId))
            {
                return this.Unauthorized();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editViewModel);
            }

            await this.jobPostsService.EditAsync(editViewModel.Id, editViewModel.Title, editViewModel.Description, editViewModel.City, editViewModel.Country);
            return this.RedirectToAction(nameof(this.Id), new { id = editViewModel.Id });
        }

        [Authorize]
        public async Task<IActionResult> Id(int id)
        {
            DetailsViewModel viewModel = this.jobPostsService.GetById<DetailsViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.EditPermission = await this.UserPermission(viewModel.Employer.Id);

            return this.View(viewModel);
        }

        /// <summary>
        /// Check if user have permission for operation.
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns>Return true, if check pass.</returns>
        private async Task<bool> UserPermission(int employerId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            return employerId == user.EmployerId || this.User.IsInRole(Common.GlobalConstants.AdministratorRoleName);
        }
    }
}
