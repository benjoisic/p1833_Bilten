using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Korisnici
{
    public class KorisniciDodajVM
    {
        public string ImeIPrezime { get; set; }

        public int JMBG { get; set; }

        public string Email { get; set; }

        public List<SelectListItem> vrsteZaposlenika { get; set; }

        public string username { get; set; }

        public string password { get; set; }
    }
}
