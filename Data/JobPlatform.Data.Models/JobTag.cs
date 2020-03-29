namespace JobPlatform.Data.Models
{
    using JobPlatform.Data.Common.Models;

    public class JobTag : BaseModel<int>
    {
        public int JobPostId { get; set; }

        public JobPost JobPost { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
