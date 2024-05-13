namespace ClientTask.Models
{
    public class MTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string status { get; set; }
        public DateTime DueDate { get; set; }
    }
    public enum Status
    {
        Success,
        InProgress,
        Completed
    }

}
