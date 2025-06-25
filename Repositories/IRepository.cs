using ToDoList.Models.Entities;

namespace ToDoList.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        List<T> GetAll();
        T? GetById(long id);
        void Add(T task);
        void Delete(long id);
        void SaveChanges();
    }
}
