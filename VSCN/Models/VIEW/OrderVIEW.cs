namespace VSCN.Models.VIEW
{
    public class OrderVIEW
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public int Acreage { get; set; }
        public bool Active { get; set; }
        public bool Trash { get; set; }
        public DateTime Time { get; set; }
        public string TimeS { get; set; }
        public string ProductName { get; set; }
    }
}
