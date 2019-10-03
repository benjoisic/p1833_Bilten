using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Mjere
{
    public class MjerePrikaziVM
    {
        public int KategorijeId { get; set; }

        public string NazivKategorije { get; set; }

        public List<Row> Rows { get; set; }

        public class Row
        {
            public int MjereId { get; set; }

            public string OpisMjere { get; set; }
        }
    }
}
