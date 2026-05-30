using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class ProductImage
{
    public int PrdImgId { get; set; }

    public int? PrdId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Product? Prd { get; set; }
}
