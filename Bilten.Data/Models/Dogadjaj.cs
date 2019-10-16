using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Bilten.Data.Models
{
    public class Dogadjaj
    {
        public int Id { get; set; }

        [ForeignKey(nameof(OrganizacionaJedinicaId))]
        public OrganizacionaJedinica OrganizacionaJedinica { get; set; }
        public int OrganizacionaJedinicaId { get; set; }

        [ForeignKey(nameof(PodorganizacionaJedinicaId))]
        public PodorganizacionaJedinica PodorganizacionaJedinica { get; set; }
        public int PodorganizacionaJedinicaId { get; set; }

        [ForeignKey(nameof(VrsteId))]
        public Vrste Vrste { get; set; }
        public int VrsteId { get; set; }

        [ForeignKey(nameof(KategorijeId))]
        public Kategorije Kategorije { get; set; }
        public int KategorijeId { get; set; }

        public DateTime? DatumDogadjaja { get; set; }
        public string MjestoDogadjaja { get; set; }
        public DateTime? DatumPrijave { get; set; }
        public string Prijavitelj { get; set; }
        public string Opis { get; set; }

        public bool? Odabran { get; set; }

        public string SlikaPath { get; set; }
    }
}
