using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;

namespace OnlineShopping.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly OnlineShoppingContext _db;

        public AdminRepository(OnlineShoppingContext db)
        {
            _db = db;
        }

        public int TotalProducts()
        {
            return _db.Products.Count();
        }
        public int TotalOrders()
        {
            return _db.Orders.Count();
        }
        public int TotalUsers()
        {
            return _db.Users.Count();
        }
        public int TotalCategories()
        {
            return _db.Categories.Count();
        }
        public int PendingOrders()
        {
            return _db.Orders.Count(o => o.Status == "Pending");
        }
        public int DeliveredOrders()
        {
            return _db.Orders.Count(o => o.Status == "Delivered");
        }
        public decimal TotalRevenue()
        {
            return _db.Orders
               .Where(o => o.Status == "Delivered")
               .Sum(o => o.TotalAmount ?? 0);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _db.Orders
               .Include(o => o.Login)
               .Include(o => o.OrderItems)
               .OrderByDescending(o => o.OrdId)
               .ToList();
        }
        public void UpdateOrderStatus(int ordId, string status)
        {
            var order = _db.Orders.Find(ordId);
            if (order != null) order.Status = status;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Categories.Include(c => c.SubCategories).ToList();
        }
        public void AddCategory(Category cat)
        { 
            _db.Categories.Add(cat);
        }
        public void DeleteCategory(int catgId)
        {
            var cat = _db.Categories.Find(catgId);
            if (cat != null) _db.Categories.Remove(cat);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}