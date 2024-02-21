using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tekton.Products.Entity.Implementations;
namespace Tekton.Products.RepositoryLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        //Propiedades
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Product>()
            .Property(p => p.price)
            .HasColumnType("decimal(18,0)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
