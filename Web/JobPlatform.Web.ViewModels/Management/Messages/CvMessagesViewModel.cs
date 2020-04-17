namespace JobPlatform.Web.ViewModels.Management.Messages
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class CvMessagesViewModel : IMapFrom<CvMessage>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CvUrl { get; set; }

        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

        public JobPost JobPost { get; set; }

        public string JobTitle => this.JobPost.Title;

        public int JobId => this.JobPost.Id;

        public string ShortMessage
        {
            get
            {
                if (string.IsNullOrEmpty(this.Message))
                {
                    return string.Empty;
                }

                string message = WebUtility.HtmlDecode(Regex.Replace(this.Message, @"<[^>]+>", string.Empty));
                return message.Length > 100
                        ? message.Substring(0, 300) + "..."
                        : message;
            }
        }
    }
}