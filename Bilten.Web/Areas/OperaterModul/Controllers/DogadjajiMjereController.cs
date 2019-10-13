using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.OperaterModul.ViewModels.DogadjajiMjere;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.OperaterModul.Controllers
{
    [Area("OperaterModul")]
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

        public IActionResult IndexZaUredi(int dogadjajId)
        {
            DogadjajiMjereIndexZaUrediVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajiMjereIndexZaUrediVM
            {
                DogadjajID = x.Id,
                Dogadjaj = x.Opis,
                Rows = _context.DogadjajiMjere.Where(y => y.DogadjajId == x.Id).Select(y => new DogadjajiMjereIndexZaUrediVM.Row
                {
                    DogadjajiMjereID = y.Id,
                    MjeraID = y.MjereId,
                    Mjera = y.Mjere.Opis,
                    Poduzeta = y.MjeraPoduzeta
                }).ToList()
            }).FirstOrDefault();

            return PartialView(model);
        }

            public IActionResult Dodaj(int dogadjajId)
        {
            DogadjajiMjereDodajVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajiMjereDodajVM
            {
                DogadjajID = x.Id,
                Poduzeta = false,

                mjere = _context.Mjere.Where(a=>a.KategorijeId == x.KategorijeId).Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Opis
                }).ToList()
        }).FirstOrDefault();


            return View(model);
        }

        public IActionResult Snimi(int dogadjajId, int mjeraId, bool poduzeta)
        {
            DogadjajiMjere DM = new DogadjajiMjere();

            DM.DogadjajId = dogadjajId;
            DM.MjereId = mjeraId;
            DM.MjeraPoduzeta = poduzeta;

            _context.DogadjajiMjere.Add(DM);
            _context.SaveChanges();

            return Redirect("/OperaterModul/Dogadjaj/Uredi?=" + DM.DogadjajId);
        }

        public IActionResult Poduzeta(int dogadjajiMjereId)
        {
            //Mjere temp = _context.Mjere.Where(x => x.Id == mjeraId).FirstOrDefault();
            DogadjajiMjere temp = _context.DogadjajiMjere.Where(x => x.Id == dogadjajiMjereId).FirstOrDefault();

            temp.MjeraPoduzeta = false;

            _context.DogadjajiMjere.Update(temp);
            _context.SaveChanges();

            //DogadjajiMjere temp = _context.DogadjajiMjere.Where(x => x.Id == dogadjajiMjereId).FirstOrDefault();

            //temp.MjeraPoduzeta = false;

            //_context.DogadjajiMjere.Update(temp);
            //_context.SaveChanges();

            return Redirect("/OperaterModul/Dogadjaj/Uredi?=" + temp.DogadjajId);
        }

        public IActionResult NijePoduzeta(int dogadjajiMjereId)
        {
            DogadjajiMjere temp = _context.DogadjajiMjere.Where(x => x.Id == dogadjajiMjereId).FirstOrDefault();

            temp.MjeraPoduzeta = true;

            _context.DogadjajiMjere.Update(temp);
            _context.SaveChanges();

            return Redirect("/OperaterModul/Dogadjaj/Uredi?=" + temp.DogadjajId);
        }
    }
}