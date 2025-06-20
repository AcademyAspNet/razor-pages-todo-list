using ToDoList.Models;

namespace ToDoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly List<UserTask> _tasks;

        public TaskService()
        {
            _tasks = new List<UserTask>();
        }

        public List<UserTask> GetTasks()
        {
            return _tasks;
        }

        public void CreateTask(string title, string? description)
        {
            UserTask task = new UserTask()
            {
                Title = title,
                Description = description
            };

            _tasks.Add(task);
        }
    }
}
