using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Dogadjaj
{
    public class DogadjajDetaljiVM
    {

        public int DogadjajID { get; set; }

            public string Kategorija { get; set; }
            public int KategorijaID { get; set; }
            public string OrganizacioneJedinice { get; set; }
            public string PodorganizacioneJedinice { get; set; }
            public string Vrste { get; set; }
            public DateTime DatumDogadjaja { get; set; }
            public string MjestoDogadjaja { get; set; }
            public DateTime DatumPrijave { get; set; }
            public string Prijavitelj { get; set; }
            public string Opis { get; set; }
            public List<string> mjere { get; set; }

    }
}
