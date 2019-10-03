using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.PodorganizacionaJedinica
{
    public class PodorganizacionaJedinicaDodajVM
    {
        public int PodorganizacionaJedinicaID { get; set; }
        public string Naziv { get; set; }
        public List<SelectListItem> orgJedinice { get; set; }
        public int OrganizacionaJedinicaID { get; set; }
        public string OrganizacionaJedinica { get; set; }
    }
}
