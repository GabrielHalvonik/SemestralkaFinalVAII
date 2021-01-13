using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public DbSet<Kryptomeny> Kryptomeny { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }

    }
}
