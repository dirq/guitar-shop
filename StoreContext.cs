using Microsoft.EntityFrameworkCore;

namespace GuitarShop;

public class StoreContext : DbContext
{
    public DbSet<Guitar> Guitars { get; set; }
    public DbSet<Musician> Musicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //This is just an example app, so we're hard-coding the connection string here.
        //In a real app, do not hard code connection strings, or store it plain text in a config file
        //Consider options like Azure Key Vault for storing secrets like connection strings
        optionsBuilder.UseSqlServer(@"Server=.\;Database=GuitarShop;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyColumnPrecisionAndDefaultsGlobally(modelBuilder);

        modelBuilder.Entity<Guitar>().ToTable("Guitars");
        modelBuilder.Entity<Musician>().ToTable("Musicians");

        base.OnModelCreating(modelBuilder);
    }



    /// <summary>
    /// Set sql server column precisions if not specifically specified in code attributes - decimals and varchar
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void ApplyColumnPrecisionAndDefaultsGlobally(ModelBuilder modelBuilder)
    {
        //decimals
        var decimalProperties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
        foreach (var property in decimalProperties)
        {
            property.SetColumnType("decimal(18, 3)");
        }

        //strings
        var stringProperties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string));
        foreach (var property in stringProperties)
        {
            if (property.GetMaxLength() == null)
            {
                //don't use MAX for varchar, it's not performant
                property.SetMaxLength(4000);
            }
        }

    }
}