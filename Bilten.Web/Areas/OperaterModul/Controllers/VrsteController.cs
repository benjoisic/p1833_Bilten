using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.OperaterModul.ViewModels.Vrste;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.OperaterModul.Controllers
{
    [Area("OperaterModul")]
    public class VrsteController : Controller
    {
        private MojContext _context;

        public VrsteController(MojContext context)
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

            VrsteIndexVM model = new VrsteIndexVM()
            {
                Rows = _context.Vrste.Select(x => new VrsteIndexVM.Row
                {
                    KategorijeId = x.KategorijeId,
                    NazivKategorije = x.Kategorije.Naziv,
                    NazivVrste = x.Naziv
                }).ToList()
            };

            return View(model);
        }

    }
}