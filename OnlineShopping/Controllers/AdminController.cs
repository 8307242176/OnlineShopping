using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;
using OnlineShopping.ViewModel;
using OnlineShopping.ViewModels;

namespace OnlineShopping.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepo;
        private readonly IProductRepository _productRepo;

        public AdminController(IAdminRepository adminRepo,
                               IProductRepository productRepo)
        {
            _adminRepo = adminRepo;
            _productRepo = productRepo;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session
                              .GetInt32("LoginId") != null;
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var vm = new AdminDashboardVM
            {
                TotalProducts = _adminRepo.TotalProducts(),
                TotalOrders = _adminRepo.TotalOrders(),
                TotalUsers = _adminRepo.TotalUsers(),
                TotalCategories = _adminRepo.TotalCategories(),
                PendingOrders = _adminRepo.PendingOrders(),
                DeliveredOrders = _adminRepo.DeliveredOrders(),
                TotalRevenue = _adminRepo.TotalRevenue()
            };
            return View(vm);
        }

        public IActionResult Products()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var products = _productRepo.GetAllProduct();
            return View(products);
        }

        public IActionResult AddProduct()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.Categories = _productRepo.GetAllCategories();
            return View(new ProductFormVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductFormVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _productRepo.GetAllCategories();
                return View(model);
            }

            var product = new Product
            {
                PrdName = model.PdName,
                PrdDescription = model.Description,
                Price = model.Price,
                CatId = model.CatgId,
                ScatgId = model.SCatgId
            };

            _productRepo.Add(product);
            _productRepo.Save();

            if (!string.IsNullOrEmpty(model.ImgUrl))
            {
                var img = new ProductImage
                {
                    PrdId = product.PrdId,
                    ImageUrl = model.ImgUrl
                };
            }

            TempData["Success"] = "Product added successfully!";
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();

            var vm = new ProductFormVM
            {
                PdId = product.PrdId,
                PdName = product.PrdName,
                Description = product.PrdDescription,
                Price = product.Price ?? 0,
                CatgId = product.CatId ?? 0,
                SCatgId = product.ScatgId ?? 0,
                ImgUrl = product.ProductImages
                                     .FirstOrDefault()?.ImageUrl
            };

            ViewBag.Categories = _productRepo.GetAllCategories();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(ProductFormVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _productRepo.GetAllCategories();
                return View(model);
            }

            var product = _productRepo.GetById(model.PdId);
            if (product == null) return NotFound();

            product.PrdName = model.PdName;
            product.PrdDescription = model.Description;
            product.Price = model.Price;
            product.CatId = model.CatgId;
            product.ScatgId = model.SCatgId;

            if (!string.IsNullOrEmpty(model.ImgUrl))
            {
                var existingImg = product.ProductImages.FirstOrDefault();
                if (existingImg != null)
                    existingImg.ImageUrl = model.ImgUrl;
            }

            _productRepo.Update(product);
            _productRepo.Save();

            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction("Products");
        }

        public IActionResult DeleteProduct(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            _productRepo.Delete(id);
            _productRepo.Save();

            TempData["Success"] = "Product deleted successfully!";
            return RedirectToAction("Products");
        }

        public IActionResult Orders()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var orders = _adminRepo.GetAllOrders();
            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int ordId, string status)
        {
            _adminRepo.UpdateOrderStatus(ordId, status);
            _adminRepo.Save();

            TempData["Success"] = "Order status updated!";
            return RedirectToAction("Orders");
        }

        public IActionResult Users()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var users = _adminRepo.GetAllUsers();
            return View(users);
        }

        public IActionResult Categories()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var cats = _adminRepo.GetAllCategories();
            return View(cats);
        }

        [HttpPost]
        public IActionResult AddCategory(string catgName)
        {
            if (!string.IsNullOrEmpty(catgName))
            {
                _adminRepo.AddCategory(new Category
                {
                    CatName = catgName
                });
                _adminRepo.Save();
                TempData["Success"] = "Category added!";
            }
            return RedirectToAction("Categories");
        }

        public IActionResult DeleteCategory(int id)
        {
            _adminRepo.DeleteCategory(id);
            _adminRepo.Save();
            TempData["Success"] = "Category deleted!";
            return RedirectToAction("Categories");
        }
        public IActionResult GetSubCategories(int catgId)
        {
            var subs = _productRepo
                .GetSubCategoriesByCatg(catgId)
                .Select(s => new { s.ScatgId, s.SubCatgName })
                .ToList();

            return Json(subs);
        }
    }
}