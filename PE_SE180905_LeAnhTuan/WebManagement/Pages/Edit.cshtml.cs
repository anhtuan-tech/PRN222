using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebManagement.Pages
{
    public class EditModel : PageModel
    {
        private readonly UserService _service = new UserService();

        [BindProperty]
        public UserEntity UserEntity { get; set; } = default!;

        public SelectList RoleOptions { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var user = _service.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            UserEntity = user;
            // Clear password for security/UI convenience (user can type a new one if they want to change it)
            UserEntity.Password = string.Empty;

            RoleOptions = new SelectList(new[] { "Staff", "User" });
            return Page();
        }

        public IActionResult OnPost()
        {
            // If password is not provided, we will retain the old password
            if (string.IsNullOrEmpty(UserEntity.Password))
            {
                ModelState.Remove("UserEntity.Password");
            }

            if (!ModelState.IsValid)
            {
                RoleOptions = new SelectList(new[] { "Staff", "User" });
                return Page();
            }

            _service.UpdateUser(UserEntity);
            return RedirectToPage("./Index");
        }
    }
}
