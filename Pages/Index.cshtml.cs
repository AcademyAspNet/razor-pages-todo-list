using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDoList.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(Name = "error", SupportsGet = true)]
        public string? ErrorMessage { get; set; }
    }
}
