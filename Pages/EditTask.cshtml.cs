using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ToDoList.Exceptions;
using ToDoList.Models.Entities;
using ToDoList.Services;

namespace ToDoList.Pages
{
    public class EditTaskModel : PageModel
    {
        private readonly ITaskService _taskService;

        public EditTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private IActionResult RedirectToHome(string? errorMessage = null)
        {
            return RedirectToPage("/Index", new {
                error = errorMessage
            });
        }

        [BindProperty(Name = "id", SupportsGet = true)]
        public long? TaskId { get; set; }

        public IActionResult OnGetChangeTaskStatus()
        {
            if (TaskId == null)
                return RedirectToHome("Не указан идентификатор задачи.");

            try
            {
                _taskService.ChangeTaskStatus((long) TaskId);
            }
            catch (TaskNotFoundException)
            {
                return RedirectToHome($"Задачи с идентификатором {TaskId} не существует.");
            }

            return RedirectToHome();
        }

        public IActionResult OnGetDeleteTask()
        {
            if (TaskId == null)
                return RedirectToHome("Не указан идентификатор задачи.");

            try
            {
                _taskService.DeleteTask((long) TaskId);
            }
            catch (TaskNotFoundException)
            {
                return RedirectToHome($"Задачи с идентификатором {TaskId} не существует.");
            }

            return RedirectToHome();
        }

        [BindProperty]
        [Required(ErrorMessage = "Задача обязательно должна содержать заголовок")]
        [MinLength(3, ErrorMessage = "Минимальная длинна заголовка задачи - 3 символа")]
        [MaxLength(64, ErrorMessage = "Максимальная длинна заголовка задачи - 64 символа")]
        public required string Title { get; set; }

        [BindProperty]
        [MaxLength(256, ErrorMessage = "Максимальная длинна описания задачи - 256 символов")]
        public string? Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Должен быть указан статус задачи")]
        public required bool IsDone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Должны быть указаны дата и время создания задачи")]
        public required DateTime CreatedAt { get; set; }

        public IActionResult OnGet()
        {
            if (TaskId == null)
                return RedirectToHome($"Не указан идентификатор задачи.");

            UserTask? task = _taskService.GetTaskById((long) TaskId);

            if (task == null)
                return RedirectToHome($"Задачи с идентификатором {TaskId} не существует.");

            Title = task.Title;
            Description = task.Description;
            IsDone = task.IsDone;
            CreatedAt = task.CreatedAt;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (TaskId == null)
                return RedirectToHome($"Не указан идентификатор задачи.");

            _taskService.UpdateTask((long) TaskId, Title, Description, IsDone, CreatedAt);

            return RedirectToHome();
        }
    }
}
