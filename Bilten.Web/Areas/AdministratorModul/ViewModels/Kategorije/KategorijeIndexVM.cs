using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Kategorije
{
    public class KategorijeIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int KategorijeId { get; set; }

            public string Naziv { get; set; }
        }
    }
}
