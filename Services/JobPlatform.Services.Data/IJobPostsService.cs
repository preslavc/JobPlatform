﻿namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;

    public interface IJobPostsService
    {
        Task<int> CreateAsync(string title, string description, string city, string country, int employerId, string tags);

        Task EditAsync(int id, string title, string description, string city, string country, string tags);

        Task DeleteAsync(JobPost jobPost);

        JobPost GetJobPost(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int? page = null);

        double GetJobCount();

        double GetJobCountByEmployer(string name);

        double GetJobCountByTag(string keyword);

        IEnumerable<T> GetAllBy<T>(string keyword, string city, int? page);

        IEnumerable<T> GetAllByTag<T>(string keyword, int? page);

        bool JobPostExist(int jobPostId);

        IEnumerable<T> GetAllByEmployer<T>(string name, int? page);

        double GetJobCountBySearch(string keyword, string city);

        Task DeleteAllPostsFromEmployer(int employerId);
    }
}
