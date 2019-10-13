using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.OperaterModul.ViewModels.PodorgJedinice
{
    public class PodorgJediniceIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int PodorganizacionaJedinicaID { get; set; }
            public string PodorganizacionaJedinica { get; set; }

            public string OrganizacionaJedinica { get; set; }
        }
    }
}
