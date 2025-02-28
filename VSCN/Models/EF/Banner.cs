using System;
using System.Collections.Generic;

namespace VSCN.Models.EF
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string? Avatar { get; set; }
        public bool? Trash { get; set; }
    }
}
