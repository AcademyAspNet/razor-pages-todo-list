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
        [Required(ErrorMessage = "������ ����������� ������ ��������� ���������")]
        [MinLength(3, ErrorMessage = "����������� ������ ��������� ������ - 3 �������")]
        [MaxLength(64, ErrorMessage = "������������ ������ ��������� ������ - 64 �������")]
        public required string Title { get; set; }

        [BindProperty]
        [MaxLength(256, ErrorMessage = "������������ ������ �������� ������ - 256 ��������")]
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
