using System;
using System.Collections.Generic;

namespace VSCN.Models.EF
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParent = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Link { get; set; }
        public bool? Trash { get; set; }
        public bool? Active { get; set; }

        public virtual Menu? Parent { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
    }
}
