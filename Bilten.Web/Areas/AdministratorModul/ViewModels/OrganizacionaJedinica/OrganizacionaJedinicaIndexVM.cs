using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.OrganizacionaJedinica
{
    public class OrganizacionaJedinicaIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int OrganizacionaJedinicaID { get; set; }
            public string Naziv { get; set; }
        }
    }
}
