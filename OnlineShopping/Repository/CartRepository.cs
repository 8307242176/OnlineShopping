using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;

namespace OnlineShopping.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly OnlineShoppingContext _context;

        public CartRepository(OnlineShoppingContext context)
        {
            _context = context;
        }

        public void AddItem(CartItem item)
        {
            _context.CartItems.Add(item);
        }

        public void ClearCart(int CartId)
        {
           var item = _context.CartItems.Where(x=>x.CartId== CartId);
            _context.CartItems.RemoveRange(item);
        }

        public void CreateCart(int loginId)
        {
            _context.Carts.Add(new Cart { LoginId = loginId });
        }

        public Cart GetCartByUser(int LoginId)
        {
           return _context.Carts.FirstOrDefault(x => x.LoginId == LoginId);
        }

        public List<CartItem> GetCartItems(int CartId)
        {
           return _context.CartItems.Include(x=>x.Prd).ThenInclude(x=>x.ProductImages).Where(x=>x.CartId== CartId).ToList();
        }

        public void RemoveItem(CartItem CartItemId)
        {
            var item = _context.CartItems.Find(CartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateQuantity(int CartItemId, int Qty)
        {
            var item = _context.CartItems.Find(CartItemId);
            if (item != null) item.Qty = Qty;
        }
    }
}
