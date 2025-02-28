using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Models.DAO
{
    public class OrderDAO
    {
        private VSCNContext context = new VSCNContext();

        public void InsertOrUpdate(Order item)
        {
            if (item.Id == 0)
            {
                context.Orders.Add(item);
            }

            context.SaveChanges();
        }

        public Order GetItem(int id)
        {
            var query = context.Orders.Where(x => x.Id == id && x.Trash == false).FirstOrDefault();
            return query;
        }
        public (int, List<OrderVIEW>) Search(string name = "",int productId=0, bool active = true,int index = 1, int size = 10)
        {
            if (name == null) name = "";

            
            var query = (from a in context.Orders
                         join b in context.Products on a.ProductId equals b.Id
                         where a.Name.Contains(name) && a.Trash == false                       
                         &&a.Active==active
                         orderby a.Id descending
                         select new OrderVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Phone=a.Phone,
                             Note=a.Note,
                             Address=a.Address,
                             Price=a.Price??0,
                             Acreage=a.Acreage??0,
                             ProductName=b.Name,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? false,
                             TimeS = a.Time.HasValue ? a.Time.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""

                         });
            if (productId > 0)
            {
                query = query.Where(o => o.Id == productId);
            }
            // Tổng số bản ghi
            int total = query.Count();

            // Áp dụng phân trang nếu cần
            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size);
            }

            return (total, query.ToList());
        }
    }
}
