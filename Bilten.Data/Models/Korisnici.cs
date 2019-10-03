using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bilten.Data.Models
{
    public class Korisnici
    {
        public int Id { get; set; }

        public string ImePrezime { get; set; }

        public int JMBG { get; set; }

        public string email { get; set; }

        [ForeignKey(nameof(VrstaKorisnikaId))]
        public VrstaKorisnika VrstaKorisnika { get; set; }
        public int VrstaKorisnikaId { get; set; }

        [ForeignKey(nameof(KorisnickiNalogId))]
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public int KorisnickiNalogId { get; set; }

    }
}
