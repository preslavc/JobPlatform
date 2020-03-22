namespace JobPlatform.Services.Data
{
    public interface IEmployerService
    {
        T GetById<T>(int id);
    }
}
