using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.OperaterModul.ViewModels.OrganizacionaJedinica;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.OperaterModul.Controllers
{
    [Area("OperaterModul")]

    public class OrgJedController : Controller
    {
        private MojContext _context;

        public OrgJedController(MojContext context)
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
            OrgJedIndexVM model = new OrgJedIndexVM()
            {
                Rows = _context.OrganizacionaJedinica.Select(x => new OrgJedIndexVM.Row
                {
                    OrganizacionaJedinicaID = x.Id,
                    Naziv = x.Naziv
                }).ToList()
            };

            return View(model);
        }
    }
}