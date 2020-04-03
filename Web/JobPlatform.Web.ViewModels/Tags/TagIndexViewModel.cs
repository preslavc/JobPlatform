namespace JobPlatform.Web.ViewModels.Tags
{
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;
    using JobPlatform.Web.ViewModels.Shared;

    public class TagIndexViewModel
    {
        public string Name { get; set; }

        public JobsDisplayViewModel JobsDisplayViewModel { get; set; }
    }
}
