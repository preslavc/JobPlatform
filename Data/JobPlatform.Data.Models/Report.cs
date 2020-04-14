namespace JobPlatform.Data.Models
{
    using JobPlatform.Data.Common.Models;

    public class Report : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int? JobPostId { get; set; }

        public virtual JobPost JobPost { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool Resolved { get; set; }

        public string ResolvedInfo { get; set; }
    }
}
