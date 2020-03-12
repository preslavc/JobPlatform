namespace JobPlatform.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Data.Common.Models;

    public class Location : BaseDeletableModel<int>
    {
        [Display(Name = "Държава")]
        public string Country { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }
    }
}
