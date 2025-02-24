using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Models.DAO
{
    public class CategoryDAO
    {
        private VSCNContext context = new VSCNContext();
        public void InsertOrUpdate(Category item)
        {
            if (item.Id == 0)
            {
                context.Categories.Add(item);
            }

            context.SaveChanges();
        }

        public Category GetItem(int id)
        {
            var query = context.Categories.Where(x => x.Id == id && x.Trash == false).FirstOrDefault();
            return query;
        }
        public Category GetItemBySlug(string slug)
        {
            var query = context.Categories.Where(x => x.Slug == slug && x.Trash == false).FirstOrDefault();
            return query;
        }

        public CategoryVIEW GetItemView(int id)
        {
            var query = (from a in context.Categories
                         where a.Id == id && a.Trash == false
                         select new CategoryVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Slug = a.Slug,
                             ParentId = a.ParentId??0,
                             Summary = a.Summary,
                             Avatar = a.Avatar,
                             Trash=a.Trash??false,
                             Active=a.Active??false
                         }).FirstOrDefault();

            return query;
        }

        // Get a list of Categories (excluding soft-deleted categories)
        public List<CategoryVIEW> GetList()
        {
            var query = (from a in context.Categories
                         where a.Trash == false
                         select new CategoryVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Slug = a.Slug,
                             ParentId = a.ParentId ?? 0,
                             Summary = a.Summary,
                             Avatar = a.Avatar,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? false
                         }).ToList();

            return query;
        }
        public (int, List<CategoryVIEW>) Search(string name = "", int index = 1, int size = 10)
        {
            if (name == null) name = "";

            // Truy vấn danh mục chính (ParentId == null)
            var query = (from a in context.Categories
                         where a.Name.Contains(name) && a.Trash == false && a.ParentId == null
                         select new CategoryVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Slug = a.Slug,
                             ParentId = a.ParentId ?? 0,
                             Summary = a.Summary,
                             Avatar = a.Avatar,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? false,
                             SubCategory = (from sub in context.Categories
                                            where sub.ParentId == a.Id && sub.Trash == false
                                            select new CategoryVIEW
                                            {
                                                Id = a.Id,
                                                Name = a.Name,
                                                Slug = a.Slug,
                                                ParentId = a.ParentId ?? 0,
                                                Summary = a.Summary,
                                                Avatar = a.Avatar,
                                                Trash = a.Trash ?? false,
                                                Active = a.Active ?? false
                                            }).ToList()
                         }).ToList();

            // Tổng số bản ghi
            int total = query.Count();

            // Áp dụng phân trang nếu cần
            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }

            return (total, query);
        }
        public void Delete(int id)
        {
            Category category = GetItem(id);
            if (category != null)
            {
                category.Trash = true;
                context.SaveChanges();
            }
        }

        public bool CheckSlug(string slug, int id)
        {
            if (id != 0)
            {
                var query = context.Categories.FirstOrDefault(x => x.Slug == slug && x.Id != id);
                return query != null;
            }
            else
            {
                var query = context.Categories.Where(x => x.Slug == slug).FirstOrDefault();
                return query != null;
            }
        }
    }
}
