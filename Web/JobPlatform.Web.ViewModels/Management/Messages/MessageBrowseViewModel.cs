namespace JobPlatform.Web.ViewModels.Management.Messages
{
    using System.Collections.Generic;

    public class MessageBrowseViewModel
    {
        public IEnumerable<CvMessagesViewModel> CvMessages { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
