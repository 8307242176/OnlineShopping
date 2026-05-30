using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class SubCategory
{
    public int ScatgId { get; set; }

    public int? CatId { get; set; }

    public string SubCatgName { get; set; } = null!;

    public virtual Category? Cat { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
