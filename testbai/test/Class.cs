using Microsoft.AspNetCore.Mvc;

namespace test.Controllers // Thay YourApp bằng tên project của bạn
{
    public class Class : Controller
    {
        // Bước 1: Chạy Action này trước để khởi tạo giá trị cho TempData
        public IActionResult SetData()
        {
            TempData["Message"] = "Hi";

            // Sau khi gán xong, chuyển hướng ngay sang Action Next
            return RedirectToAction("Next");
        }

        // Bước 2: Action này sẽ nhận request tiếp theo và tiến hành đọc TempData
        public IActionResult Next()
        {
            // Đọc lần 1
            var msg = TempData["Message"];

            // Đọc lần 2 (vẫn trong cùng request hiện tại)
            var again = TempData["Message"];

            // Trả về kết quả trực tiếp lên trình duyệt
            return Content($"{msg} | {again}");
        }
    }
}