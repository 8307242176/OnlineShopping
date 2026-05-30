using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;

namespace OnlineShopping.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShoppingContext _context;

        public ProductRepository(OnlineShoppingContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.Include(p => p.ProductImages).Include(p => p.Scatg).ToList();

              
         
        }
        public IEnumerable<Category> GetAllCategories() =>
    _context.Categories.ToList();

        public IEnumerable<SubCategory> GetSubCategoriesByCatg(int catgId) =>
            _context.SubCategories.Where(s => s.CatId == catgId).ToList();

        public IEnumerable<Product> GetByCategory(int catgId) =>
       _context.Products
          .Include(p => p.ProductImages)  
          .Include(p => p.Scatg)
          .Where(p => p.CatId == catgId)
          .ToList();

        public Product GetById(int id) =>
     _context.Products
        .Include(p => p.ProductImages)  
        .Include(p => p.Cat)            
        .Include(p => p.Scatg)          
        .FirstOrDefault(p => p.PrdId == id);

        public IEnumerable<Product> GetBySubCategory(int ScatgId)
        {
           return _context.Products.Include(x=>x.ProductImages).Where(x=>x.ScatgId == ScatgId).ToList();
        }
        public int GetTotalCount() { 
    return _context.Products.Count();
        }
        public void Save()
        {
           _context.SaveChanges();
        }

        public IEnumerable<Product> Search(string keyword)
        {
            return _context.Products.Include(x => x.ProductImages).Where(x => x.PrdName.Contains(keyword)).ToList();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
