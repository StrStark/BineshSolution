using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;


namespace DataBaseManager.DbContexts;

public class SalesDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

    public DbSet<Sales> Sales { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Carpet> Carpets { get; set; } = default!;
    public DbSet<Rug> Rugs { get; set; } = default!;
    public DbSet<RawMaterial> RawMaterials { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().UseTpcMappingStrategy();

        modelBuilder.Entity<Carpet>().ToTable("Carpets");
        modelBuilder.Entity<Rug>().ToTable("Rugs");
        modelBuilder.Entity<RawMaterial>().ToTable("RawMaterials");

                
    }
}
