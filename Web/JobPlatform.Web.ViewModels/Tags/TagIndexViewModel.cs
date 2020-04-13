namespace JobPlatform.Web.ViewModels.Tags
{
    using JobPlatform.Web.ViewModels.Browse;

    public class TagIndexViewModel
    {
        public string Name { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public JobsDisplayViewModel JobsDisplayViewModel { get; set; }
    }
}
