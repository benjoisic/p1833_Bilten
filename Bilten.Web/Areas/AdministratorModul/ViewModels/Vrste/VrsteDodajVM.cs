using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Vrste
{
    public class VrsteDodajVM
    {
        public int KategorijeId { get; set; }

        [Required(ErrorMessage = "Tekst ne smije ostati prazana !")]
        public string NazivKategorije { get; set; }

        public int VrsteId { get; set; }

        public string Naziv { get; set; }
    }
}
