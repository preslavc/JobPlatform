namespace JobPlatform.Web.ViewModels.Job
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Data.Models;

    public class CreateViewModel
    {
        [Display(Name = "Позиция")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public Location Location { get; set; }
    }
}
