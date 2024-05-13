using ClientTask.Models;

namespace ClientTask.Interfaces
{
    public interface IDeserialiseTask
    {
        public IEnumerable<MTask> EnumerableDeserialise(string ApiReponse);
        public MTask Deserialise(string ApiReponse);
    }
}
