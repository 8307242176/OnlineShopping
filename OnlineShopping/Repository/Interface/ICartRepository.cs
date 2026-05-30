using OnlineShopping.Models;

namespace OnlineShopping.Repository.Interface
{
    public interface ICartRepository
    {
        Cart GetCartByUser(int LoginId);
        void CreateCart(int LoginId);
        void AddItem(CartItem item);
        void RemoveItem(CartItem item);
        void UpdateQuantity(int CartItemId, int Qty);
        List<CartItem> GetCartItems(int CartId);
        void ClearCart(int CartId);
        void Save();
    }
}
