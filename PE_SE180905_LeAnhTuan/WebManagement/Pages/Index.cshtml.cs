using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserService _service = new UserService();
        public List<UserEntity> Users { get; set; } = default!;

        public void OnGet()
        {
            Users = _service.GetUsers();
        }

        // Xử lý xóa bằng AJAX không tải lại trang
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                _service.DeleteUser(id);
                return new JsonResult(new { success = true });
            }
            catch
            {
                return new JsonResult(new { success = false });
            }
        }
    }
}