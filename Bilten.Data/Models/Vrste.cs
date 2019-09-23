using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bilten.Data.Models
{
    public class Vrste
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        [ForeignKey(nameof(KategorijeId))]
        public Kategorije Kategorije { get; set; }
        public int KategorijeId { get; set; }
    }
}
