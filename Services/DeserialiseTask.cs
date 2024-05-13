using ClientTask.Interfaces;
using ClientTask.Models;
using Newtonsoft.Json;

namespace ClientTask.Services
{
    //deserialiser une chaine en un objet .net 
    public class DeserialiseTask:IDeserialiseTask
    {
        private MTask _mTask { get; set; }
        public DeserialiseTask()
        {
           _mTask = new MTask();
        }

        public IEnumerable <MTask> EnumerableDeserialise(string ApiReponse) => JsonConvert.DeserializeObject<IEnumerable<MTask>>(ApiReponse);//deserialser une collection

        public MTask Deserialise(string ApiReponse)=> JsonConvert.DeserializeObject<MTask>(ApiReponse);//deserialiser un objet 
        
    }
}
