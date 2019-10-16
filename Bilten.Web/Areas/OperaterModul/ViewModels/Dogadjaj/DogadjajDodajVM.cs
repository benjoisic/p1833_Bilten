using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.OperaterModul.ViewModels.Dogadjaj
{
    public class DogadjajDodajVM
    {
        public int KategorijaID { get; set; }
        public string Kategorija { get; set; }

        public List<SelectListItem> OrganizacioneJedinice { get; set; }
        public List<SelectListItem> PodorganizacioneJedinice { get; set; }
        public List<SelectListItem> Vrste { get; set; }
        public DateTime DatumDogadjaja { get; set; }
        public string MjestoDogadjaja { get; set; }
        public DateTime DatumPrijave { get; set; }
        public string Prijavitelj { get; set; }
        public string Opis { get; set; }
        public List<string> mjere { get; set; }
        public IFormFile SlikaFF { get; set; }
    }
}
