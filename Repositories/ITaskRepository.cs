using ToDoList.Models.Entities;

namespace ToDoList.Repositories
{
    public interface ITaskRepository
    {
        List<UserTask> GetAll();
        UserTask? GetById(long id);
        void Add(UserTask task);
        void Delete(long id);
        void SaveChanges();
    }
}
