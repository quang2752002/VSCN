using VSCN.Models.EF;
using System.Linq;

namespace VSCN.Models.DAO
{
    public class InformationDAO
    {
        private VSCNContext context = new VSCNContext();

        public string getItem(string name)
        {
            var query = context.Information
                               .Where(x => x.Name == name)
                               .Select(x => x.Value) // Assuming `Value` is the string you want
                               .FirstOrDefault();

            return query ?? ""; 
        }
    }
}
