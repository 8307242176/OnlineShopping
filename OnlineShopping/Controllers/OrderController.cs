using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;

namespace OnlineShopping.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;

        public OrderController(IOrderRepository orderRepo, ICartRepository cartRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
        }



        public IActionResult PlaceOrder()
        {
            int? LoginId = HttpContext.Session.GetInt32("LoginID");
            if (LoginId == null)
            {
                return RedirectToAction("Login","Account");
            }
            var cart = _cartRepo.GetCartByUser(LoginId.Value);
                if(cart == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            var cartItem = _cartRepo.GetCartItems(cart.CartId);
            if (!cartItem.Any())
            {
                return RedirectToAction("Index", "Cart");
            }
            var order = new Order
            {
                LoginId = LoginId.Value,
                TotalAmount = (int)cartItem.Sum(x => (x.Prd?.Price ?? 0) * (x.Qty)),
                Status = "Pending"
            };

                var orderItems = cartItem.Select(x => new OrderItem
                {
                    PrdId = x.PrdId ?? 0,
                    Quantity = x.Qty??0 ,
                    Price = (int)(x.Prd?.Price ?? 0)
                }).ToList();
            _orderRepo.PlaceOrder(order, orderItems);
            _cartRepo.ClearCart(cart.CartId);
            _cartRepo.Save();
            

            return RedirectToAction("MyOrders");

        }
        public IActionResult MyOrders()
        {
            int? loginId = HttpContext.Session.GetInt32("LoginID");
            if (loginId == null) return RedirectToAction("Login", "Account");

            var orders = _orderRepo.GetOrdersByUser(loginId.Value);
            return View(orders);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
