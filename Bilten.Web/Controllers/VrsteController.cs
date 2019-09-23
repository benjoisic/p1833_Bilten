using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.ViewModels.Vrste;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Controllers
{
    public class VrsteController : Controller
    {
        private MojContext _context;

        public VrsteController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            VrsteIndexVM model = new VrsteIndexVM()
            {
                Rows = _context.Kategorije.Select(x => new VrsteIndexVM.Row
                {
                    KategorijeId = x.Id,
                    NazivKategorije = x.Naziv
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Odaberi(int KategorijeId)
        {
            VrsteOdaberiVM model = _context.Kategorije.Where(x => x.Id == KategorijeId).Select(x => new VrsteOdaberiVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv,
                Rows = _context.Vrste.Where(y=>y.KategorijeId == x.Id).Select(y=> new VrsteOdaberiVM.Row
                {
                    VrsteId = y.Id,
                    Naziv = y.Naziv
                }).ToList()
            }).FirstOrDefault();

            return View(model);
        }
        
        public IActionResult Dodaj(int kategorijeId)
        {
            VrsteDodajVM model = _context.Kategorije.Where(x => x.Id == kategorijeId).Select(x => new VrsteDodajVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Snimi(string nazivVrste, int kategorijeId)
        {
            Vrste novo = new Vrste();
            novo.Naziv = nazivVrste;
            novo.KategorijeId = kategorijeId;

            _context.Vrste.Add(novo);
            _context.SaveChanges();

            return Redirect("/Vrste/Odaberi?=" + kategorijeId);
        }

        public IActionResult Obrisi(int vrsteId)
        {
            Vrste temp = _context.Vrste.Where(x => x.Id == vrsteId).FirstOrDefault();

            _context.Vrste.Remove(temp);
            _context.SaveChanges();

            return Redirect("/Vrste/Odaberi?=" + temp.KategorijeId);
        }

        public IActionResult Uredi(int vrsteId)
        {
            VrsteDodajVM model = _context.Vrste.Where(x => x.Id == vrsteId).Select(x => new VrsteDodajVM
            {
                VrsteId = vrsteId,
                Naziv = x.Naziv,
                KategorijeId = x.KategorijeId
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult SnimiPromjene(int kategorijeId, int vrsteId, string nazivVrste)
        {
            Vrste novo = _context.Vrste.Where(x => x.Id == vrsteId).FirstOrDefault();
            novo.Naziv = nazivVrste;
            novo.KategorijeId = kategorijeId;

            _context.Vrste.Update(novo);
            _context.SaveChanges();

            return Redirect("/Vrste/Odaberi?=" + kategorijeId);
        }
    }
}