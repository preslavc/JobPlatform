namespace JobPlatform.Web.ViewModels.Management.Messages
{
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobApplyViewModel : IMapFrom<JobPost>
    {
        public string Title { get; set; }

        public int EmployerId { get; set; }

        public IEnumerable<CvMessagesViewModel> CvMessages { get; set; }
    }
}
