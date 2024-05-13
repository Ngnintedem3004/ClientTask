using ClientTask.Interfaces;
using ClientTask.Models;

namespace ClientTask.Services
{
    //effectuer des actions http 
    public class PerformHttpClient : IPerformHttpClient<MTask>
    {
        private readonly HttpClient _httpClient;
        public string url {  get; private set; }
        public PerformHttpClient() {
            _httpClient = new HttpClient();
        }
      
       

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }


        public Task PostAsync(string url, StringContent content)
        {
            return _httpClient.PostAsync(url, content);
        }

        public Task PutAsync(string url, MultipartFormDataContent content)
        {
            return _httpClient.PutAsync(url, content);
        }

        

        Task IPerformHttpClient<MTask>.DeleteAsync(string url)
        {
           
            return _httpClient.DeleteAsync(url);
        }
    }
}
