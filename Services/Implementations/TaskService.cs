using ToDoList.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly List<UserTask> _tasks;
        private long _nextTaskId;

        public TaskService()
        {
            _tasks = new List<UserTask>();
            _nextTaskId = 0;
        }

        public List<UserTask> GetTasks()
        {
            return _tasks;
        }

        public void CreateTask(string title, string? description)
        {
            UserTask task = new UserTask()
            {
                Id = _nextTaskId++,
                Title = title,
                Description = description
            };

            _tasks.Add(task);
        }

        public UserTask? GetTaskById(long id)
        {
            foreach (UserTask task in _tasks)
            {
                if (task.Id == id)
                    return task;
            }

            return null;
        }

        public void ChangeTaskStatus(long id, bool isDone)
        {
            UserTask? task = GetTaskById(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            task.IsDone = isDone;
        }

        public void ChangeTaskStatus(long id)
        {
            UserTask? task = GetTaskById(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            task.IsDone = !task.IsDone;
        }

        public void DeleteTask(long id)
        {
            UserTask? task;

            for (int i = 0; i < _tasks.Count; i++)
            {
                task = _tasks[i];

                if (task == null)
                    continue;

                if (task.Id == id)
                {
                    _tasks.RemoveAt(i);
                    return;
                }
            }

            throw new TaskNotFoundException(id);
        }
    }
}
