namespace JobPlatform.Web.Areas.Management.Controller
{
    using JobPlatform.Common;
    using JobPlatform.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.EmployerRoleName)]
    [Area("Management")]
    public class ManagementController : BaseController
    {
    }
}
