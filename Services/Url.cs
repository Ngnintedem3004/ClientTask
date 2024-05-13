using ClientTask.Interfaces;

namespace ClientTask.Services
{
    //recupérer  l'url contenant les endpoints
    public class Url 
    {
        public static string GetUrl(string url)
        {
            return url;
        }
        public static string GetUrl(string  url ,int id)
        {
            return url + "/" + id;
        }
    }
}
