using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;

namespace OnlineShopping.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineShoppingContext _context;

        public UserRepository(OnlineShoppingContext context)
        {
            _context = context;
        }

        public bool EmailExist(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public User GetByEmail(string email)
        {
         return   _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.LoginId == id);
        }

        public void Register(User user)
        {
           _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
