using Microsoft.EntityFrameworkCore;

namespace GuitarShop;

public class StoreContext : DbContext
{
    //public DbSet<Guitar> Guitars { get; set; }
    //public DbSet<Musician> Musicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //This is just an example app, so we're hard-coding the connection string here.
        //In a real app, do not hard code connection strings, or store it plain text in a config file
        //Consider options like Azure Key Vault for storing secrets like connection strings
        optionsBuilder.UseSqlServer(@"Server=.\;Database=GuitarStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    //    modelBuilder.Entity<Guitar>().ToTable("Guitars");

        base.OnModelCreating(modelBuilder);

        
    }
}