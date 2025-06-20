using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ToDoList.Services;

namespace ToDoList.Pages
{
    public class CreateTaskModel : PageModel
    {
        private readonly ITaskService _taskService;

        public CreateTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Задача обязательно должна содержать заголовок")]
        [MinLength(3, ErrorMessage = "Минимальная длинна заголовка задачи - 3 символа")]
        [MaxLength(64, ErrorMessage = "Максимальная длинна заголовка задачи - 64 символа")]
        public required string Title { get; set; }

        [BindProperty]
        [MaxLength(256, ErrorMessage = "Максимальная длинна описания задачи - 256 символов")]
        public string? Description { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _taskService.CreateTask(Title, Description);

            return RedirectToPage("/Index");
        }
    }
}
