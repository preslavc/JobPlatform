namespace JobPlatform.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using JobPlatform.Common;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Data;
    using JobPlatform.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserService applicationUserService;
        private readonly IEmployerService employerService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IApplicationUserService applicationUserService,
            IEmployerService employerService)
        {
            this.userManager = userManager;
            this.applicationUserService = applicationUserService;
            this.employerService = employerService;
        }

        public IActionResult CreateUser()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
            };

            IdentityResult result = await this.userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                if (viewModel.Role != null)
                {
                    await this.userManager.AddToRoleAsync(user, viewModel.Role);
                }

                this.TempData["InfoMessage"] = "User created successfully!";
            }

            return this.Redirect("/Administration/Dashboard/Users/");
        }

        public IActionResult CreateEmployer()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployer(EmployerViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var user = new ApplicationUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                Employer = new Employer
                {
                    Eik = viewModel.Eik,
                    Name = viewModel.EmployerName,
                    City = viewModel.City,
                    Country = viewModel.Country,
                    ImageUrl = GlobalConstants.DefaultEmployerImage,
                },
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
            };

            IdentityResult result = await this.userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(viewModel.Role))
                {
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.EmployerRoleName);
                }

                this.TempData["InfoMessage"] = "Employer created successfully!";
            }

            return this.Redirect("/Administration/Dashboard/Users/");
        }

        public async Task<IActionResult> Profile(string userId)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            if (user.EmployerId != null)
            {
                user.Employer = this.employerService.GetById((int)user.EmployerId);
            }

            var roles = await this.userManager.GetRolesAsync(user);
            string role = string.Empty;

            foreach (var r in roles)
            {
                role += r;
            }

            UserViewModel viewModel = new UserViewModel
            {
                UserId = userId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Employer = user.Employer,
                Role = role,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            if (user.EmployerId != null)
            {
                user.Employer = this.employerService.GetById((int)user.EmployerId);
            }

            var roles = await this.userManager.GetRolesAsync(user);
            string role = string.Empty;

            foreach (var r in roles)
            {
                role += r;
            }

            EditUserViewModel viewModel = new EditUserViewModel
            {
                UserId = userId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = role,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            ApplicationUser user = await this.userManager.FindByIdAsync(viewModel.UserId);
            if (user == null)
            {
                return this.NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            string role = string.Empty;

            foreach (var r in roles)
            {
                role += r;
            }

            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                string token = await this.userManager.GeneratePasswordResetTokenAsync(user);
                await this.userManager.ResetPasswordAsync(user, token, viewModel.Password);
            }

            string roleViewModel = viewModel.Role == null ? string.Empty : viewModel.Role;
            if (role != roleViewModel)
            {
                if (role != string.Empty)
                {
                    await this.userManager.RemoveFromRoleAsync(user, role);
                }

                if (roleViewModel != string.Empty)
                {
                    await this.userManager.AddToRoleAsync(user, roleViewModel);
                }
            }

            if (user.Email != viewModel.Email)
            {
                string token = await this.userManager.GenerateChangeEmailTokenAsync(user,viewModel.Email);
                await this.userManager.ChangeEmailAsync(user, viewModel.Email, token);
            }

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.PhoneNumber = viewModel.PhoneNumber;
            await this.userManager.UpdateAsync(user);

            this.TempData["InfoMessage"] = "User update successfully!";
            return this.Redirect($"/Administration/User/{user.Id}");
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            string role = string.Empty;

            foreach (var r in roles)
            {
                role += r;
            }

            if (role != string.Empty)
            {
                await this.userManager.RemoveFromRoleAsync(user, role);
            }

            await this.userManager.DeleteAsync(user);
            this.TempData["InfoMessage"] = "User deleted successfully!";
            return this.Redirect("/Administration/Dashboard");
        }

        public IActionResult Search(string name)
        {
            UserBrowseViewModel viewModel = new UserBrowseViewModel
            {
                SearchUserViewModel = new SearchUserViewModel
                {
                    Name = name,
                },
                Users = this.applicationUserService.GetUsersByName<UserCardViewModel>(name),
            };
            return this.View(viewModel);
        }
    }
}
