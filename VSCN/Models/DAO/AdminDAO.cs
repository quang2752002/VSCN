using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Models.DAO
{
    public class AdminDAO
    {
        private VSCNContext context = new VSCNContext();

        public int Login(string username, string password)
        {
            var query = (from a in context.Admins
                         where a.Username == username && a.Password == password
                         select new AdminVIEW
                         {
                             Id = a.Id,

                         }).FirstOrDefault();

            if (query != null)
                return query.Id;
            return -1;

        }
    }
}
