using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Areas.OperaterModul.ViewModels.DogadjajiMjere
{
    public class DogadjajiMjereDodajVM
    {
        public int DogadjajID { get; set; }

        public List<SelectListItem> mjere { get; set; }

        public bool Poduzeta { get; set; }
    }
}
