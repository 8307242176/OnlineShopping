using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;
using System.Security.Cryptography;

namespace OnlineShopping.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineShoppingContext _context;

        public OrderRepository(OnlineShoppingContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(x=>x.Login).Include(x=>x.OrderItems).ToList();
        }

        public Order GetOrderById(int OrdId)
        {
           return _context.Orders.Include(x => x.OrderItems).ThenInclude(oi => oi.Prd).FirstOrDefault(o => o.OrdId == OrdId);


        }

        public IEnumerable<Order> GetOrdersByUser(int LoginId)
        {
            return _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Prd).ThenInclude(p => p.ProductImages).Where(x => x.LoginId == LoginId).OrderByDescending(o => o.OrdId).ToList();

        }

        public void PlaceOrder(Order order, List<OrderItem> items)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            items.ForEach(x => x.OrdId = order.OrdId);
            _context.OrderItems.AddRange(items);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateStatus(int OrdId, string Status)
        {
            var order = _context.Orders.Find(OrdId);
            if (order != null)
            {
                order.Status = Status;
            }
        }
    }
}
