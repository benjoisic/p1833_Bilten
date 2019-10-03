using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bilten.Web.Models;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Helper;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]
    public class HomeController : Controller
    {

        private readonly MojContext _context;

        public HomeController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            return View();
        }
    }
}
