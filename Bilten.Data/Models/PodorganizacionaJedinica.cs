using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bilten.Data.Models
{
    public class PodorganizacionaJedinica
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        [ForeignKey(nameof(OrganizacionaJedinicaId))]
        public OrganizacionaJedinica OrganizacionaJedinica { get; set; }
        public int OrganizacionaJedinicaId { get; set; }
    }
}
