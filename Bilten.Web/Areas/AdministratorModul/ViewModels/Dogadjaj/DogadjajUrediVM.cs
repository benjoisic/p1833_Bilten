using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Dogadjaj
{
    public class DogadjajUrediVM
    {
        public int DogadjajID { get; set; }
        public string Kategorija { get; set; }
        public int KategorijaID { get; set; }
        public List<SelectListItem> OrganizacioneJedinice { get; set; }
        public int OrgJedID { get; set; }
        public List<SelectListItem> PodorganizacioneJedinice { get; set; }
        public int PodOrgID { get; set; }
        public List<SelectListItem> Vrste { get; set; }
        public int VrsteID { get; set; }
        public DateTime DatumDogadjaja { get; set; }
        public string MjestoDogadjaja { get; set; }
        public string DatumPrijave { get; set; }
        public string Prijavitelj { get; set; }
        public string Opis { get; set; }
        public string Slika { get; set; }
        public IFormFile SlikaFF { get; set; }
        public string VrsteTekst { get; set; }
        public string OrgJedTekst { get; set; }
        public string PodOrgTekst { get; set; }
    }
}
