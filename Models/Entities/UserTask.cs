namespace ToDoList.Models.Entities
{
    public class UserTask : Entity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
