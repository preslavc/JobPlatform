namespace JobPlatform.Data.Models
{
    using System.Collections.Generic;

    using JobPlatform.Data.Common.Models;

    public class Employer : BaseDeletableModel<int>
    {
        public Employer()
        {
            this.JobPosts = new HashSet<JobPost>();
        }

        public string Name { get; set; }

        public string Eik { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<JobPost> JobPosts { get; set; }
    }
}
