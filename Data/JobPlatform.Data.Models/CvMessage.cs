namespace JobPlatform.Data.Models
{
    using JobPlatform.Data.Common.Models;

    public class CvMessage : BaseDeletableModel<int>
    {
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int JobPostId { get; set; }

        public virtual JobPost JobPost { get; set; }

        public string CvUrl { get; set; }

        public string Message { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
