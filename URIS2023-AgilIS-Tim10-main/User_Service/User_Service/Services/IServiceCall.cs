namespace User_Service.Services
{
    public interface IServiceCall<T>
    {
        Task<T> SendGetRequestAsync(string url);

    }
}
