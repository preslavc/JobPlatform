namespace JobPlatform.Data.Models
{
    using System.Collections.Generic;

    using JobPlatform.Data.Common.Models;

    public class JobPost : BaseDeletableModel<int>
    {
        public JobPost()
        {
            this.Tags = new HashSet<JobTag>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public int EmployerId { get; set; }

        public Employer Employer { get; set; }

        public virtual ICollection<JobTag> Tags { get; set; }
    }
}
