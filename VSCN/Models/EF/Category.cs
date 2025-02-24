using System;
using System.Collections.Generic;

namespace VSCN.Models.EF
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Summary { get; set; }
        public string? Avatar { get; set; }
        public bool? Trash { get; set; }
        public bool? Active { get; set; }

        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
