using System.Text.Json;
using ToDoList.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Repositories.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _entities;
        private long _nextId;

        protected Repository()
        {
            try
            {
                string json = File.ReadAllText(GetFilePath());
                List<T>? tasks = JsonSerializer.Deserialize<List<T>>(json);

                if (tasks == null)
                    throw new JsonException("Failed to deserialize list of tasks from JSON data");

                _entities = tasks;
            }
            catch (Exception)
            {
                _entities = new List<T>();
            }

            long maxId = 0;

            foreach (T entity in _entities)
            {
                if (entity.Id > maxId)
                    maxId = entity.Id;
            }

            _nextId = maxId + 1;
        }

        protected abstract string GetFilePath();

        public List<T> GetAll()
        {
            return _entities;
        }

        public T? GetById(long id)
        {
            foreach (T entity in _entities)
            {
                if (entity.Id == id)
                    return entity;
            }

            return null;
        }

        public void Add(T entity)
        {
            entity.Id = _nextId++;

            _entities.Add(entity);
            SaveChanges();
        }

        public void Delete(long id)
        {
            T? entity;

            for (int i = 0; i < _entities.Count; i++)
            {
                entity = _entities[i];

                if (entity == null)
                    continue;

                if (entity.Id == id)
                {
                    _entities.RemoveAt(i);
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

            string json = JsonSerializer.Serialize(_entities, options);
            File.WriteAllText(GetFilePath(), json);
        }
    }
}
