namespace JobPlatform.Web.ViewModels.Management.Messages
{

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobApplyViewModel : MessageBrowseViewModel, IMapFrom<JobPost>
    {
        public string Title { get; set; }

        public int EmployerId { get; set; }
    }
}
