using OnlineShopping.Models;

namespace OnlineShopping.Repository.Interface
{
    public interface IAdminRepository
    {
        int TotalProducts();
        int TotalOrders();
        int TotalUsers();
        int TotalCategories();
        int PendingOrders();
        int DeliveredOrders();
        decimal TotalRevenue();

        IEnumerable<User> GetAllUsers();

        IEnumerable<Order> GetAllOrders();
        void UpdateOrderStatus(int ordId, string status);


        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category cat);
        void DeleteCategory(int catgId);

        void Save();
    }
}