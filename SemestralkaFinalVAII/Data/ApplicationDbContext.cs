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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

    }
}
