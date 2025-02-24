using System;
using System.Collections.Generic;

namespace VSCN.Models.EF
{
    public partial class Information
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
