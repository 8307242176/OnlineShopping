using OnlineShopping.Models;

namespace OnlineShopping.Repository.Interface
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order, List<OrderItem> items);
        IEnumerable<Order> GetOrdersByUser(int LoginId);
        Order GetOrderById(int OrdId);
        void UpdateStatus(int OrdId, string Status);
        IEnumerable<Order> GetAllOrders();
        void Save();

    }
}
