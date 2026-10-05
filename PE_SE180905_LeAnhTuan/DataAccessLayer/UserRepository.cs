using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserRepository
    {
        private readonly UserManagerDbContext _context = new UserManagerDbContext();

        public List<UserEntity> GetAll() => _context.UserEntities.ToList();
        public UserEntity? GetById(int id) => _context.UserEntities.Find(id);
        public UserEntity? GetByUserName(string username) => _context.UserEntities.FirstOrDefault(u => u.UserName == username);

        public void Add(UserEntity user)
        {
            _context.UserEntities.Add(user);
            _context.SaveChanges();
        }

        public void Update(UserEntity user)
        {
            _context.UserEntities.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.UserEntities.Find(id);
            if (user != null)
            {
                _context.UserEntities.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
