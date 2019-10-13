using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.OperaterModul.ViewModels.OrganizacionaJedinica
{
    public class OrgJedIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int OrganizacionaJedinicaID { get; set; }
            public string Naziv { get; set; }

        }
    }
}
