namespace JobPlatform.Web.ViewModels.Jobs
{
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class TagViewModel : IMapFrom<JobTag>
    {
        public Tag Tag { get; set; }

        public string Name => this.Tag.Name;
    }
}
