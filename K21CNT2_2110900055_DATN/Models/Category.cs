using System;
using System.Collections.Generic;

namespace K21CNT2_2110900055_DATN.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
