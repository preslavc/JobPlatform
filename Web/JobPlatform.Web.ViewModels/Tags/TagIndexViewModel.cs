namespace JobPlatform.Web.ViewModels.Tags
{
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class TagIndexViewModel
    {
        public string Name { get; set; }

        public IEnumerable<TaggedJobsViewModel> JobPosts { get; set; }
    }
}
