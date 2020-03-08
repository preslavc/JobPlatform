namespace JobPlatform.Data.Models
{
    using System.Collections.Generic;

    using JobPlatform.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Jobs = new HashSet<JobTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<JobTag> Jobs { get; set; }
    }
}
