namespace JobPlatform.Web.ViewModels.Administration.Users
{
    using System;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class UserCardViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
