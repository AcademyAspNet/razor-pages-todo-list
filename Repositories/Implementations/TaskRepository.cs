using System.Text.Json;
using ToDoList.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private const string FILE_PATH = "tasks.json";

        private readonly List<UserTask> _tasks;
        private long _nextId;

        public TaskRepository()
        {
            try
            {
                string json = File.ReadAllText(FILE_PATH);
                List<UserTask>? tasks = JsonSerializer.Deserialize<List<UserTask>>(json);

                if (tasks == null)
                    throw new JsonException("Failed to deserialize list of tasks from JSON data");

                _tasks = tasks;
            }
            catch (Exception)
            {
                _tasks = new List<UserTask>();
            }

            long maxId = 0;

            foreach (UserTask task in _tasks)
            {
                if (task.Id > maxId)
                    maxId = task.Id;
            }

            _nextId = maxId + 1;
        }

        public List<UserTask> GetAll()
        {
            return _tasks;
        }

        public UserTask? GetById(long id)
        {
            foreach (UserTask task in _tasks)
            {
                if (task.Id == id)
                    return task;
            }

            return null;
        }

        public void Add(UserTask task)
        {
            task.Id = _nextId++;

            _tasks.Add(task);
            SaveChanges();
        }

        public void Delete(long id)
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
                    SaveChanges();

                    return;
                }
            }

            throw new TaskNotFoundException(id);
        }

        public void SaveChanges()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(_tasks, options);
            File.WriteAllText(FILE_PATH, json);
        }
    }
}
