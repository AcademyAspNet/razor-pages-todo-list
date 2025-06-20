using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        List<UserTask> GetTasks();
        void CreateTask(string title, string? description);
    }
}
