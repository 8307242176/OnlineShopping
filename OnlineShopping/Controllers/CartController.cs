using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;
using OnlineShopping.ViewModel;

namespace OnlineShopping.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public IActionResult Index()
        {
            int? loginId = HttpContext.Session.GetInt32("LoginID");
            if (loginId == null)
                return RedirectToAction("Login", "Account");

            var cart = _cartRepo.GetCartByUser(loginId.Value);
            if (cart == null)
                return View(new List<CartVM>());

            var items = _cartRepo.GetCartItems(cart.CartId)
                .Select(ci => new CartVM
                {
                    CartItemId = ci.CartItemId,
                    PdID = ci.PrdId ?? 0,
                    PdName = ci.Prd?.PrdName ?? "Unknown",
                    ImgURL = ci.Prd?.ProductImages
                                       .FirstOrDefault()?.ImageUrl,
                    Price = (int)(ci.Prd?.Price ?? 0),
                    Quantity = ci.Qty ?? 1
                }).ToList();

            return View(items);
        }
        //public IActionResult AddToCart(int pdId)
        //{
        //    // DEBUG - see what session has
        //    var sessionValue = HttpContext.Session.GetInt32("LoginID");
        //    var allKeys = HttpContext.Session.Keys;

        //    // temporarily return content to see session state
        //    return Content($"LoginId in session = {sessionValue}, pdId = {pdId}, Keys = {string.Join(", ", allKeys)}");
        //}
        public IActionResult AddToCart(int pdId)
        {
            int? loginId = HttpContext.Session.GetInt32("LoginID");
            if (loginId == null)
                return RedirectToAction("Login", "Account");


            var cart = _cartRepo.GetCartByUser(loginId.Value);
            if (cart == null)
            {
                _cartRepo.CreateCart(loginId.Value);
                _cartRepo.Save();
                cart = _cartRepo.GetCartByUser(loginId.Value);
            }

            var cartItems = _cartRepo.GetCartItems(cart.CartId);
            var existingItem = cartItems
                .FirstOrDefault(i => i.PrdId == pdId);

            if (existingItem != null)
            {

                _cartRepo.UpdateQuantity(
                    existingItem.CartItemId,
                    (existingItem.Qty ?? 1) + 1);
            }
            else
            {

                _cartRepo.AddItem(new CartItem
                {
                    CartId = cart.CartId,
                    PrdId = pdId,
                    Qty = 1
                });
            }

            _cartRepo.Save();
            return RedirectToAction("Index");
        }
    }
}