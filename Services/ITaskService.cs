using ToDoList.Models.Entities;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        List<UserTask> GetTasks();
        void CreateTask(string title, string? description);
        UserTask? GetTaskById(long id);
        void ChangeTaskStatus(long id, bool isDone);
        void ChangeTaskStatus(long id);
        void DeleteTask(long id);
    }
}
