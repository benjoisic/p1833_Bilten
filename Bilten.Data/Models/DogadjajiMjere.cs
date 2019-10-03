using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bilten.Data.Models
{
    public class DogadjajiMjere
    {
        public int Id { get; set; }

        [ForeignKey(nameof(DogadjajId))]
        public Dogadjaj Dogadjaj { get; set; }
        public int DogadjajId { get; set; }

        [ForeignKey(nameof(MjereId))]
        public Mjere Mjere{ get; set; }
        public int MjereId { get; set; }

        public bool MjeraPoduzeta { get; set; }
    
    }
}
