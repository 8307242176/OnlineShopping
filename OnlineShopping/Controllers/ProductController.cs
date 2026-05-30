using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Repository.Interface;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public IActionResult Index(int? catgId, int? sCatgId, string keyword)
        {
            var products = catgId.HasValue? _productRepo.GetByCategory(catgId.Value) : sCatgId.HasValue ? _productRepo.GetBySubCategory(sCatgId.Value) : !string.IsNullOrEmpty(keyword) ? _productRepo.Search(keyword) : _productRepo.GetAllProduct();
            ViewBag.Categories = _productRepo.GetAllCategories();
            ViewBag.SelectedCatg = catgId;
            ViewBag.Keyword = keyword;

            return View(products);

        }
        public IActionResult Detail(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();
            ViewBag.RelatedProducts = _productRepo
                .GetByCategory(product.CatId ?? 0)
                .Where(p => p.PrdId != id)
                .Take(4).ToList();

            return View(product);
        }

        public IActionResult Search(string keyword)
        {
            var results = _productRepo.Search(keyword);
            return View("Index", results);
        }


        public IActionResult ByCategory(int id)
        {
            var products = _productRepo.GetByCategory(id);
            return View("Index", products);
        }
    }
}
