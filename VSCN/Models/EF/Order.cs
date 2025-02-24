using System;
using System.Collections.Generic;

namespace VSCN.Models.EF
{
    public partial class Order
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public string? Address { get; set; }
        public int? Price { get; set; }
        public int? Acreage { get; set; }
        public bool? Active { get; set; }
        public bool? Trash { get; set; }
        public DateTime? Time { get; set; }

        public virtual Product? Product { get; set; }
    }
}
