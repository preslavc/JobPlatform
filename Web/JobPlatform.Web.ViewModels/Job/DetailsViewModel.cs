namespace JobPlatform.Web.ViewModels.Job
{
    using System;
    using System.Collections.Generic;

    public class DetailsViewModel
    {
        // Test data
        public DetailsViewModel()
        {
            this.Title = "Software Developer";
            this.City = "София";
            this.Country = "България";
            this.ImageUrl = "https://softuni.bg/companies/profile/logo/134";
            this.Employer = "FirmaBG";
            this.PublishDate = DateTime.UtcNow.Date.ToShortDateString();
            this.Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            this.Tags = new List<string> { "C#", "Web Development", "Java Development", "Python" };
        }

        public string Title { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Employer { get; set; }

        public string ImageUrl { get; set; }

        public string PublishDate { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
