namespace ClientTask.Interfaces
{
    public interface IPerformHttpClient<MTask>
    {
        Task DeleteAsync( string url);
        Task<HttpResponseMessage> GetAsync(string url );
        Task PostAsync(string url,StringContent content);
        Task PutAsync (string url,MultipartFormDataContent content);
        
    }
}
