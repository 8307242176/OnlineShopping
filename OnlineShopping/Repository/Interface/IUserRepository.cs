using OnlineShopping.Models;

namespace OnlineShopping.Repository.Interface
{
    public interface IUserRepository
    {
        User GetByEmail(string Email);
        User GetById(int id);
        bool EmailExist(string Email);
        void Register(User user);
        void Save();
    }
}
