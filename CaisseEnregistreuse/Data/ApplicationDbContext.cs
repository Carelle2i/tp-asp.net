using Microsoft.EntityFrameworkCore;
using CaisseEnregistreuse.Models;

namespace CaisseEnregistreuse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        // Cette méthode est utilisée pour configurer les entités et les relations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la propriété Prix avec précision et échelle
            modelBuilder.Entity<Produit>()
                .Property(p => p.Prix)
                .HasColumnType("decimal(18,2)"); 
        }
    }
}