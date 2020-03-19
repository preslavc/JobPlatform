﻿namespace JobPlatform.Web.Attributes
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
            : base()
        {
            this.Roles = string.Join(",", roles);
        }
    }
}
