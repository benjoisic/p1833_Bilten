using System;
using System.Collections.Generic;
using System.Text;

namespace Bilten.Data.Models
{
    public class Korisnici
    {
        public int Id { get; set; }

        public string ImePrezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set;  }
    }
}
