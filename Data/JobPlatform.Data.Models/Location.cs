namespace JobPlatform.Data.Models
{
    using JobPlatform.Data.Common.Models;

    public class Location : BaseDeletableModel<int>
    {
        public string Country { get; set; }

        public string City { get; set; }
    }
}
