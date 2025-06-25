using ToDoList.Exceptions;
using ToDoList.Models.Entities;
using ToDoList.Repositories;

namespace ToDoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<UserTask> _taskRepository;

        public TaskService(IRepository<UserTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public List<UserTask> GetTasks()
        {
            return _taskRepository.GetAll();
        }

        public void CreateTask(string title, string? description)
        {
            UserTask task = new UserTask()
            {
                Title = title,
                Description = description
            };

            _taskRepository.Add(task);
        }

        public void UpdateTask(long id, string title, string? description, bool isDone, DateTime createdAt)
        {
            UserTask? task = _taskRepository.GetById(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            task.Title = title;
            task.Description = description;
            task.IsDone = isDone;
            task.CreatedAt = createdAt;

            _taskRepository.SaveChanges();
        }

        public UserTask? GetTaskById(long id)
        {
            return _taskRepository.GetById(id);
        }

        public void ChangeTaskStatus(long id, bool isDone)
        {
            UserTask? task = _taskRepository.GetById(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            task.IsDone = isDone;
            _taskRepository.SaveChanges();
        }

        public void ChangeTaskStatus(long id)
        {
            UserTask? task = _taskRepository.GetById(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            task.IsDone = !task.IsDone;
            _taskRepository.SaveChanges();
        }

        public void DeleteTask(long id)
        {
            _taskRepository.Delete(id);
        }
    }
}
