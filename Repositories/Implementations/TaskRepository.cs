using ToDoList.Models.Entities;

namespace ToDoList.Repositories.Implementations
{
    public class TaskRepository : Repository<UserTask>
    {
        protected override string GetFilePath()
        {
            return "tasks.json";
        }
    }
}
