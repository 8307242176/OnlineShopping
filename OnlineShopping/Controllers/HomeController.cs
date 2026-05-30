using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;
using System.Diagnostics;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnlineShoppingContext _context;
        private readonly IProductRepository _prodRepo;

        public HomeController(OnlineShoppingContext context, IProductRepository prodRepo)
        {
            _context=context;
            _prodRepo = prodRepo;
        }

       
        public IActionResult Index()
        {
            var product = _prodRepo.GetAllProduct().Take(8).ToList();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
