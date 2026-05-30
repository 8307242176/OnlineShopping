using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ViewModels
{
    public class ProductFormVM
    {
        public int PdId { get; set; }

        [Required]
        public string PdName { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CatgId { get; set; }

        [Required]
        public int SCatgId { get; set; }
        public string ImgUrl { get; set; }
    }
}