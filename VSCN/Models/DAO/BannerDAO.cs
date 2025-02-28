using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Models.DAO
{
    public class BannerDAO
    {
        private VSCNContext context = new VSCNContext();
        public void InsertOrUpdate(Banner item)
        {
            if (item.Id == 0)
            {
                context.Banners.Add(item);
            }

            context.SaveChanges();
        }

        public Banner GetItem(int id)
        {
            var query = context.Banners.Where(x => x.Id == id).FirstOrDefault();
            return query;
        }
        public (int, List<BannerVIEW>) ShowList()
        {
            var query = (from a in context.Banners
                        
                             
                         select new BannerVIEW
                         {
                             Id = a.Id,
                           Avatar=a.Avatar
                            
                         }).ToList();

            int total = query.Count();

            return (total, query);
        }
    }
}
