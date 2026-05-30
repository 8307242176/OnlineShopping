using OnlineShopping.Models;

namespace OnlineShopping.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProduct();
        IEnumerable<Product> GetByCategory(int CatId);
        IEnumerable<Product> GetBySubCategory(int ScatgId);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<SubCategory> GetSubCategoriesByCatg(int catgId);
        int GetTotalCount();
        IEnumerable<Product> Search(string keyword);
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        void Save();
      
    }
}
