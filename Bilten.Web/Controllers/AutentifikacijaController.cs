using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Web.ViewModels;
using Bilten.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Bilten.Web.Helper;

namespace Bilten.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private MojContext _context;

        public AutentifikacijaController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiPassword = false
            });
        }

        public IActionResult Login(LoginVM input)
        {
            KorisnickiNalog korisnik = _context.KorisnickiNalog
                .Where(x => x.Username == input.username && x.Lozinka == input.password).FirstOrDefault();

            if (korisnik == null)
            {
                TempData["error_poruka"] = "pogrešan username ili password";
                return View("Index", input);
            }

            HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);

            bool isAdministrator = false, isOperater = false, isKontrolor = false;

            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();

            if(k.VrstaKorisnikaId == 1)
            {
                isAdministrator = true;
            }
            if(k.VrstaKorisnikaId == 2)
            {
                isOperater = true;
            }
            if(k.VrstaKorisnikaId == 3)
            {
                isKontrolor = true;
            }

            if(isAdministrator)
            {
                return RedirectToAction("Index", "Home", new { area = "AdministratorModul" });
            }

            if (isOperater)
            {
                return RedirectToAction("Index", "Home", new { area = "OperaterModul" });
            }

            if (isKontrolor)
            {
                return RedirectToAction("Index", "Home", new { area = "KontrolorModul" });
            }



            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}