using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.OperaterModul.ViewModels.Mjere;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.OperaterModul.Controllers
{
    [Area("OperaterModul")]
    public class MjereController : Controller
    {
        private MojContext _context;

        public MjereController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            MjereIndexVM model = new MjereIndexVM()
            {
                Rows = _context.Mjere.Select(x => new MjereIndexVM.Row
                {
                    KategorijeId = x.KategorijeId,
                    NazivKategorije = x.Kategorije.Naziv,
                    NazivMjere = x.Opis
                }).ToList()
            };

            return View(model);
        }

    }
}