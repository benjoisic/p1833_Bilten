using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bilten.Data.Models
{
    public class KorisnickiNalog
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Lozinka { get; set; }
    }
}
