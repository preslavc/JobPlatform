namespace JobPlatform.Web.ViewModels.Management.Messages
{
    using System.Net;
    using System.Text.RegularExpressions;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class CvMessagesViewModel : IMapFrom<CvMessage>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CvUrl { get; set; }

        public string Message { get; set; }

        public string ShortMessage
        {
            get
            {
                string message = WebUtility.HtmlDecode(Regex.Replace(this.Message, @"<[^>]+>", string.Empty));
                return message.Length > 100
                        ? message.Substring(0, 300) + "..."
                        : message;
            }
        }
    }
}