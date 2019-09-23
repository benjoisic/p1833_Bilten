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
                ZapamtiPassword = true
            });
        }

        public IActionResult Login(LoginVM input)
        {
            Korisnici korisnik = _context.Korisnici.SingleOrDefault(x => x.KorisnickoIme == input.username && x.Lozinka == input.password);

            if(korisnik == null)
            {
                TempData["error_poruka"] = "pogrešan username ili password";
                return View("Index", input);
            }

            HttpContext.SetLogiraniKorisnik(korisnik);
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }
}