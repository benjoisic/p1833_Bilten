using Bilten.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilten.Web.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalog korisnik, bool snimiUCookie = false)
        {
            context.Session.Set(LogiraniKorisnik, korisnik);
        }

        public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
        {
            KorisnickiNalog korisnik = context.Session.Get<KorisnickiNalog>(LogiraniKorisnik);
            return korisnik;
        }
    }
}
