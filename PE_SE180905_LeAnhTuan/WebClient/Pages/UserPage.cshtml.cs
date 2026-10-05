using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace WebClient.Pages
{
    public class UserPageModel : PageModel
    {
        private readonly UserService _service = new UserService();
        public string Name { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Login");
            }

            var user = _service.GetUsers().FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login");
            }

            Name = user.FullName;
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}