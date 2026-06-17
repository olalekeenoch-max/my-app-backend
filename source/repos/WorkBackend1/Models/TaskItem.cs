namespace WorkBackend1.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int UserId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; } = "Medium";
    }
}