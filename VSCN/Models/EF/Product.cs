using System;
using System.Collections.Generic;

namespace VSCN.Models.EF
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? TypeArticle { get; set; }
        public string? Content { get; set; }
        public string? Avatar { get; set; }
        public bool? Trash { get; set; }
        public bool? Active { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
