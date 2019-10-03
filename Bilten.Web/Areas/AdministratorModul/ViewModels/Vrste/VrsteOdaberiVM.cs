using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Vrste
{
    public class VrsteOdaberiVM
    {
        public int KategorijeId { get; set; }

        public string NazivKategorije { get; set; }

        public List<Row> Rows { get; set; }

        public class Row
        {
            public int VrsteId { get; set; }

            public string Naziv { get; set; }
        }
    }
}
