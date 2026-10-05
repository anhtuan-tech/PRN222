using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _service = new UserService();

        [BindProperty]
        public string Username { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var user = _service.VerifyLogin(Username, Password);
            if (user == null)
            {
                Message = "Incorrect username or password!";
                return Page();
            }

            // Lưu tên đầy đủ của người dùng vào Session bộ nhớ
            HttpContext.Session.SetString("UserFullName", user.FullName);
            HttpContext.Session.SetString("UserName", user.UserName);

            // Điều hướng dựa vào vai trò phân quyền (Role-based redirection)
            if (user.Role == "Staff")
            {
                return RedirectToPage("/StaffPage");
            }
            else
            {
                return RedirectToPage("/UserPage");
            }
        }
    }
}