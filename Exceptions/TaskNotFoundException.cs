namespace ToDoList.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public long TaskId { get; private set; }

        public TaskNotFoundException(long taskId) : base($"Task with id {taskId} not found")
        {
            TaskId = taskId;
        }
    }
}
