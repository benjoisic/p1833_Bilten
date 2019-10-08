using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.OperaterModul.ViewModels.Mjere
{
    public class MjereIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int KategorijeId { get; set; }

            public string NazivKategorije { get; set; }

            public string NazivMjere { get; set; }
        }

    }
}
