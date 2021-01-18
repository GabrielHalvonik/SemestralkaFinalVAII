using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemestralkaFinalVAII.Models;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public DbSet<Kryptomeny> Kryptomeny { get; set; }
        public DbSet<ZoznamOblubenych> Oblubene { get; set; }
        public DbSet<HistoriaCeny> Historia { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            //builder.Entity<ZoznamOblubenych>().
            //    HasMany(p => p.Oblubene).
            //    WithMany(p => null).
            //    UsingEntity(j => j.ToTable("Kryptomeny"));
            //    //builder.Entity<ZoznamOblubenych>().HasMany(temp => temp.Oblubene);
        }
    }
}
