namespace JobPlatform.Web.ViewModels.Administration.Users
{
    using System.ComponentModel.DataAnnotations;

    public class SearchUserViewModel
    {
        [Display(Prompt = "Username or Email")]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
