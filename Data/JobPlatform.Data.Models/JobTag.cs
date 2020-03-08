namespace JobPlatform.Data.Models
{
    public class JobTag
    {
        public int JobPostId { get; set; }

        public JobPost JobPost { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
