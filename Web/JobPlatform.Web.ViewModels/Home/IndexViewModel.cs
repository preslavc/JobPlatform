namespace JobPlatform.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<JobPostViewModel> JobPosts { get; set; }
    }
}
