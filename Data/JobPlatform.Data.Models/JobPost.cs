namespace JobPlatform.Data.Models
{
    using System.Collections.Generic;

    using JobPlatform.Data.Common.Models;

    public class JobPost : BaseDeletableModel<int>
    {
        public JobPost()
        {
            this.Tags = new HashSet<JobTag>();
            this.CvMessages = new HashSet<CvMessage>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public virtual ICollection<JobTag> Tags { get; set; }

        public virtual ICollection<CvMessage> CvMessages { get; set; }
    }
}
