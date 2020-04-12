namespace JobPlatform.Web.ViewModels.Browse
{
    using System.Collections.Generic;

    public class EmployersDisplayViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<EmployerViewModel> Employers { get; set; }
    }
}
