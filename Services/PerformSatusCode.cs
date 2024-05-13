namespace ClientTask.Services
{
    public class PerformSatusCode
    {
         //verifie si  le  statut de la reponse correspond à ok
        public static bool IsOk(HttpResponseMessage response)
        {
            return response.StatusCode==System.Net.HttpStatusCode.OK;
        }
    }
    
        
    
}
