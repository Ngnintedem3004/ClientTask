using Microsoft.AspNetCore.Http;

namespace ClientTask.Services
{
    //serailiser la reponse http du message en une chaine 
    public class SerializeContentTask
    {

        public   async static Task<string> SerialiseHttpContent(HttpResponseMessage response)
        {
            return  await response.Content.ReadAsStringAsync();
        }
    }
}
