using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.ViewModels.Mjere;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bilten.Web.Controllers
{
    public class MjereController : Controller
    {
        private MojContext _context;

        public MjereController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            MjereIndexVM model = new MjereIndexVM()
            {
                Rows = _context.Kategorije.Select(x => new MjereIndexVM.Row
                {
                    KategorijeId = x.Id,
                    NazivKategorije = x.Naziv
                }).ToList()
            };


            return View(model);
        }

        public IActionResult Prikazi(int KategorijeId)
        {
            MjerePrikaziVM model = _context.Kategorije.Where(x => x.Id == KategorijeId).Select(x => new MjerePrikaziVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv,
                Rows = _context.Mjere.Where(y => y.KategorijeId == x.Id).Select(y => new MjerePrikaziVM.Row
                {
                    MjereId = y.Id,
                    OpisMjere = y.Opis
                }).ToList()
            }).FirstOrDefault();

            return View(model);
        }

        //    public IActionResult Dodaj(int kategorijeId)
        //    {
        //        VrsteDodajVM model = _context.Kategorije.Where(x => x.Id == kategorijeId).Select(x => new VrsteDodajVM
        //        {
        //            KategorijeId = x.Id,
        //            NazivKategorije = x.Naziv
        //        }).FirstOrDefault();

        //        return View(model);
        //    }

        //    public IActionResult Snimi(string nazivVrste, int kategorijeId)
        //    {
        //        Vrste novo = new Vrste();
        //        novo.Naziv = nazivVrste;
        //        novo.KategorijeId = kategorijeId;

        //        _context.Vrste.Add(novo);
        //        _context.SaveChanges();

        //        return Redirect("/Vrste/Odaberi?=" + kategorijeId);
        //    }

        //    public IActionResult Obrisi(int vrsteId)
        //    {
        //        Vrste temp = _context.Vrste.Where(x => x.Id == vrsteId).FirstOrDefault();

        //        _context.Vrste.Remove(temp);
        //        _context.SaveChanges();

        //        return Redirect("/Vrste/Odaberi?=" + temp.KategorijeId);
        //    }

        //    public IActionResult Uredi(int vrsteId)
        //    {
        //        VrsteDodajVM model = _context.Vrste.Where(x => x.Id == vrsteId).Select(x => new VrsteDodajVM
        //        {
        //            VrsteId = vrsteId,
        //            Naziv = x.Naziv,
        //            KategorijeId = x.KategorijeId
        //        }).FirstOrDefault();

        //        return View(model);
        //    }

        //    public IActionResult SnimiPromjene(int kategorijeId, int vrsteId, string nazivVrste)
        //    {
        //        Vrste novo = _context.Vrste.Where(x => x.Id == vrsteId).FirstOrDefault();
        //        novo.Naziv = nazivVrste;
        //        novo.KategorijeId = kategorijeId;

        //        _context.Vrste.Update(novo);
        //        _context.SaveChanges();

        //        return Redirect("/Vrste/Odaberi?=" + kategorijeId);
        //    }
    }
}