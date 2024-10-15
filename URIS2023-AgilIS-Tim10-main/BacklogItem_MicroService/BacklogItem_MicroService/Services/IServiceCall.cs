namespace BacklogItem_MicroService.Services
{
    public interface IServiceCall<T>
    {
        Task<T> SendGetRequestAsync(string url, string token);
    }
}
