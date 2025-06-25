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
                return RedirectToHome("�� ������ ������������� ������.");

            try
            {
                _taskService.ChangeTaskStatus((long) TaskId);
            }
            catch (TaskNotFoundException)
            {
                return RedirectToHome($"������ � ��������������� {TaskId} �� ����������.");
            }

            return RedirectToHome();
        }

        public IActionResult OnGetDeleteTask()
        {
            if (TaskId == null)
                return RedirectToHome("�� ������ ������������� ������.");

            try
            {
                _taskService.DeleteTask((long) TaskId);
            }
            catch (TaskNotFoundException)
            {
                return RedirectToHome($"������ � ��������������� {TaskId} �� ����������.");
            }

            return RedirectToHome();
        }

        [BindProperty]
        [Required(ErrorMessage = "������ ����������� ������ ��������� ���������")]
        [MinLength(3, ErrorMessage = "����������� ������ ��������� ������ - 3 �������")]
        [MaxLength(64, ErrorMessage = "������������ ������ ��������� ������ - 64 �������")]
        public required string Title { get; set; }

        [BindProperty]
        [MaxLength(256, ErrorMessage = "������������ ������ �������� ������ - 256 ��������")]
        public string? Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "������ ���� ������ ������ ������")]
        public required bool IsDone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "������ ���� ������� ���� � ����� �������� ������")]
        public required DateTime CreatedAt { get; set; }

        public IActionResult OnGet()
        {
            if (TaskId == null)
                return RedirectToHome($"�� ������ ������������� ������.");

            UserTask? task = _taskService.GetTaskById((long) TaskId);

            if (task == null)
                return RedirectToHome($"������ � ��������������� {TaskId} �� ����������.");

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
                return RedirectToHome($"�� ������ ������������� ������.");

            _taskService.UpdateTask((long) TaskId, Title, Description, IsDone, CreatedAt);

            return RedirectToHome();
        }
    }
}
