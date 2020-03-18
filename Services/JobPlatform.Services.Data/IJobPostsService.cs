namespace JobPlatform.Services.Data
{
    public interface IJobPostsService
    {
        T GetById<T>(int id);
    }
}
