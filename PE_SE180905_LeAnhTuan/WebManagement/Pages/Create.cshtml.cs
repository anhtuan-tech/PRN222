using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebManagement.Pages
{
    public class CreateModel : PageModel
    {
        private readonly UserService _service = new UserService();

        [BindProperty]
        public UserEntity UserEntity { get; set; } = default!;

        // Chuẩn bị tập dữ liệu cho DropdownList
        public SelectList RoleOptions { get; set; } = default!;

        public void OnGet()
        {
            RoleOptions = new SelectList(new[] { "Staff", "User" });
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                RoleOptions = new SelectList(new[] { "Staff", "User" });
                return Page();
            }

            _service.RegisterUser(UserEntity); // Tự động băm mật khẩu tại đây
            return RedirectToPage("./Index");
        }
    }
}