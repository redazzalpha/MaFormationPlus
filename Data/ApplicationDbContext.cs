using MaFormaPlusCoreMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaFormaPlusCoreMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conseiller>? Conseillers { get; set; }
        public DbSet<Module>? Modules { get; set; }
        public DbSet<Parcours>? Parcours { get; set; }
        public DbSet<Session>? Sessions { get; set; }
        public DbSet<Stagiaire>? Stagiaires { get; set; }
        public DbSet<Unite>? Unites { get; set; }
        public DbSet<ModuleParcours>? ModuleParcours { get; set; }

    }
}