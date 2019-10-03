using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.PodorganizacionaJedinica
{
    public class PodorganizacionaJedinicaIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int PodorganizacionaJedinicaID { get; set; }
            public string PodorganizacionaJedinica { get; set; }
        }
    }
}
