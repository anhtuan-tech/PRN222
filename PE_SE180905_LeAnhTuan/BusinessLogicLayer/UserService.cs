using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserService
    {
        private readonly UserRepository _repo = new UserRepository();
        // Đối tượng thực hiện chức năng băm mật khẩu bảo mật
        private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public List<UserEntity> GetUsers() => _repo.GetAll();
        public UserEntity? GetUserById(int id) => _repo.GetById(id);

        // Chức năng thêm mới: Tự động băm mật khẩu thô trước khi lưu vào DB
        public void RegisterUser(UserEntity user)
        {
            user.Password = _hasher.HashPassword(new object(), user.Password);
            _repo.Add(user);
        }

        public void DeleteUser(int id) => _repo.Delete(id);

        // Chức năng cập nhật: Cập nhật thông tin và băm mật khẩu nếu mật khẩu bị thay đổi
        public void UpdateUser(UserEntity user)
        {
            var existing = _repo.GetById(user.Id);
            if (existing != null)
            {
                if (!string.IsNullOrEmpty(user.Password) && user.Password != existing.Password)
                {
                    user.Password = _hasher.HashPassword(new object(), user.Password);
                }
                else
                {
                    user.Password = existing.Password;
                }

                existing.UserName = user.UserName;
                existing.FullName = user.FullName;
                existing.Email = user.Email;
                existing.Role = user.Role;
                existing.Password = user.Password;

                _repo.Update(existing);
            }
        }


        // Chức năng xác thực đăng nhập: So khớp mật khẩu thô đầu vào và chuỗi đã băm trong DB
        public UserEntity? VerifyLogin(string username, string password)
        {
            var user = _repo.GetByUserName(username);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(new object(), user.Password.Trim(), password);
            if (result == PasswordVerificationResult.Success)
            {
                return user; // Đúng mật khẩu
            }
            return null; // Sai mật khẩu
        }
    }
}
