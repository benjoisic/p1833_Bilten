using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.AdministratorModul.ViewModels.Dogadjaj
{
    public class DogadjajListaVM
    {
 

            public List<Row> Rows { get; set; }

        public class Row { 

        public int DogadjajID { get; set; }

        public string Kategorije { get; set; }

        public string Vrste { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DatumDogadjaja { get; set; }

        public string MjestoDogadjaja { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DatumPrijave { get; set; }

        public string Prijavitelj { get; set; }

        public string Opis { get; set; }

            public string podorg { get; set; }
    }
    }
}
