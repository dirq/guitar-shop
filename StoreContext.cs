using Microsoft.EntityFrameworkCore;

namespace GuitarShop;

public class StoreContext : DbContext
{
    public DbSet<Guitar> Guitars { get; set; }
    public DbSet<Musician> Musicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\;Database=GuitarStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
    }
}