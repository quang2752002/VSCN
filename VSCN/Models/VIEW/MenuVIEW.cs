namespace VSCN.Models.VIEW
{
    public class MenuVIEW
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Link { get; set; }
        public bool Trash { get; set; }
        public bool Active { get; set; }
        public List<MenuVIEW> SubMenu { get; set; }

    }
}
