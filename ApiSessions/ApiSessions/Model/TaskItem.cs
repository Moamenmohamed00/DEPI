namespace ApiSessions.Model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Pirority { get; set; } = "high";
        public string Status {  get; set; }
        public DateTime Created { get; set; }
    }
}
