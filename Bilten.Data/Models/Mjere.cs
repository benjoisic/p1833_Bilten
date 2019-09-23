using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bilten.Data.Models
{
    public class Mjere
    {
        public int Id { get; set; }

        public string Opis { get; set; }

        [ForeignKey(nameof(KategorijeId))]
        public Kategorije Kategorije { get; set; }
        public int KategorijeId { get; set; }
    }
}
