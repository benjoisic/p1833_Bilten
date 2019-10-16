using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.KontrolorModul.ViewModels;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.KontrolorModul.Controllers
{
    [Area("KontrolorModul")]
    public class DogadjajiMjereController : Controller
    {
            private MojContext _context;

            public DogadjajiMjereController(MojContext context)
            {
                _context = context;
            }

            public IActionResult Index(int dogadjajId)
        {
          
            DogadjajiMjereIndexVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajiMjereIndexVM
            {
                DogadjajID = x.Id,
                Dogadjaj = x.Opis,
                Rows = _context.DogadjajiMjere.Where(y=>y.DogadjajId == x.Id && y.MjeraPoduzeta == true).Select(y=> new DogadjajiMjereIndexVM.Row
                {
                    DogadjajiMjereID = y.Id,
                    MjeraID = y.MjereId,
                    Mjera = y.Mjere.Opis,
                    Poduzeta = y.MjeraPoduzeta
                }).ToList()
            }).FirstOrDefault();

            return PartialView(model);
        }
    }
}