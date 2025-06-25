using ToDoList.Models.Entities;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        List<UserTask> GetTasks();
        void CreateTask(string title, string? description);
        void UpdateTask(long id, string title, string? description, bool isDone, DateTime createdAt);
        UserTask? GetTaskById(long id);
        void ChangeTaskStatus(long id, bool isDone);
        void ChangeTaskStatus(long id);
        void DeleteTask(long id);
    }
}
