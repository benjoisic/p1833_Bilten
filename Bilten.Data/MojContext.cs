using Bilten.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bilten.Data
{
    public class MojContext:DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
         : base(options)
        {
        }

        public DbSet<Kategorije> Kategorije { get; set; }
        public DbSet<Vrste> Vrste { get; set; }
        public DbSet<Mjere> Mjere { get; set; }
        public DbSet<Korisnici> Korisnici{ get; set; }
    }
}
