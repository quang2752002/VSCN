namespace VSCN.Models.VIEW
{
    public class CategoryVIEW
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Avatar { get; set; }
        public bool Trash { get; set; }
        public bool Active { get; set; }
        public List<CategoryVIEW>? SubCategory { get; set; } // Mảng bài viết

    }
}
