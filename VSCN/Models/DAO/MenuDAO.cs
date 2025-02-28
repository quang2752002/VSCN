using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Models.DAO
{
    public class MenuDAO
    {
        private VSCNContext context = new VSCNContext();
        public void InsertOrUpdate(Menu item)
        {
            if (item.Id == 0)
            {
                context.Menus.Add(item);
            }

            context.SaveChanges();
        }

        public Menu GetItem(int id)
        {
            var query = context.Menus.Where(x => x.Id == id && x.Trash == false).FirstOrDefault();
            return query;
        }
        public (int, List<MenuVIEW>) ShowList()
        {
            var query = (from a in context.Menus
                         where a.Trash == false &&
                               a.Active == true &&
                               a.ParentId == null
                         select new MenuVIEW
                         {
                             Id = a.Id,
                             ParentId = a.ParentId ?? 0,
                             Name = a.Name,
                             Slug = a.Slug,
                             Link = a.Link,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? true,
                             SubMenu = context.Menus
                                 .Where(sub => sub.ParentId == a.Id && sub.Trash == false && sub.Active == true)
                                 .Select(sub => new MenuVIEW
                                 {
                                     Id = sub.Id,
                                     ParentId = sub.ParentId ?? 0,
                                     Name = sub.Name,
                                     Slug = sub.Slug,
                                     Link = sub.Link,
                                     Trash = sub.Trash ?? false,
                                     Active = sub.Active ?? true
                                 }).ToList()
                         }).ToList();

            int total = query.Count();

            return (total, query);
        }

    }
}
