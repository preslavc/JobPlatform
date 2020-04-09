namespace JobPlatform.Data.Models
{
    using JobPlatform.Data.Common.Models;

    public class JobTag : BaseModel<int>
    {
        public int JobPostId { get; set; }

        public virtual JobPost JobPost { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
