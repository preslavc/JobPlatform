namespace JobPlatform.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    public class UserBrowseViewModel
    {
        public UserBrowseViewModel()
        {
            this.SearchUserViewModel = new SearchUserViewModel();
        }

        public IEnumerable<UserCardViewModel> Users { get; set; }

        public SearchUserViewModel SearchUserViewModel { get; set; }
    }
}
