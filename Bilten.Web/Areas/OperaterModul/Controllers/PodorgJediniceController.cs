using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.OperaterModul.ViewModels.PodorgJedinice;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.OperaterModul.Controllers
{
    [Area("OperaterModul")]

    public class PodorgJediniceController : Controller
    {
        private MojContext _context;

        public PodorgJediniceController(MojContext context)
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
            PodorgJediniceIndexVM model = new PodorgJediniceIndexVM()
            {
                Rows = _context.PodorganizacionaJedinica.Select(x => new PodorgJediniceIndexVM.Row
                {
                    PodorganizacionaJedinicaID = x.Id,
                    PodorganizacionaJedinica = x.Naziv,
                    OrganizacionaJedinica = x.OrganizacionaJedinica.Naziv
                }).ToList()
            };

            return View(model);
        }
    }
}