using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using JobPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using JobPlatform.Common;
using JobPlatform.Web.Infrastructure.ValidationAttributes;

namespace JobPlatform.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class cRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public cRegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [StringLength(50, ErrorMessage = "{0} трябва да е с минимална дължина {2} или максимална {1} ", MinimumLength = 4)]
            [Display(Name = "Име на компанията")]
            public string EmployerName { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [StringLength(50, ErrorMessage = "{0} трябва да е с минимална дължина {2} или максимална {1} ", MinimumLength = 3)]
            [Display(Name = "Град")]
            public string City { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [StringLength(50, ErrorMessage = "{0} трябва да е с минимална дължина {2} или максимална {1} ", MinimumLength = 3)]
            [Display(Name = "Държава")]
            public string Country { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [EikValidation]
            [Display(Name = "ЕИК/БУЛСТАТ")]
            public string Eik { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [StringLength(50, ErrorMessage = "{0} трябва да е с минимална дължина {2} или максимална {1} ", MinimumLength = 3)]
            [Display(Name = "Име")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [StringLength(50, ErrorMessage = "{0} трябва да е с минимална дължина {2} или максимална {1} ", MinimumLength = 3)]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [Display(Name = "Телефон за връзка")]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [EmailAddress(ErrorMessage = GlobalConstants.ErrorMessageEmailField)]
            [Display(Name = "Имейл адрес")]
            public string Email { get; set; }

            [Required(ErrorMessage = GlobalConstants.ErrorMessageRequiredField)]
            [MinLength(6, ErrorMessage = "Минималната дължина трябва да е {1} символа")]
            [MaxLength(100, ErrorMessage = "Максимална дължина трябва да е {1} символа")]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърдете паролата")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Employer = new Employer
                    {
                        Eik = Input.Eik,
                        Name = Input.EmployerName,
                        City = Input.City,
                        Country = Input.Country,
                    },
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    _userManager.AddToRoleAsync(user, GlobalConstants.EmployerRoleName);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
