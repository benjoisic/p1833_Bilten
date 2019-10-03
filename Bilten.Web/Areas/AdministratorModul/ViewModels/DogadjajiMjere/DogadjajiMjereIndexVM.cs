using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.DogadjajiMjere
{
    public class DogadjajiMjereIndexVM
    {
        public int DogadjajID { get; set; }

        public string Dogadjaj { get; set; }

        public List<Row> Rows { get; set; }

        public class Row
        {
            public int DogadjajiMjereID { get; set; }

            public int MjeraID{ get; set; }

            public string Mjera { get; set; }

            public bool Poduzeta { get; set; }
        }
    }
}
