﻿namespace JobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;

    using Ganss.XSS;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class DetailsViewModel : IMapFrom<JobPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public Employer Employer { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Date => this.CreatedOn.ToString("dd.MM.yyyy");

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public bool EditPermission { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
