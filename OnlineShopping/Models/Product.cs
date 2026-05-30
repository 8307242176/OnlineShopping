using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class Product
{
    public int PrdId { get; set; }

    public int? ScatgId { get; set; }

    public string PrdName { get; set; } = null!;

    public string PrdDescription { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? CatId { get; set; }

    public int StockQty { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Cat { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual SubCategory? Scatg { get; set; }
}
