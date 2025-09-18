using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager.DbContexts;

public class InventoryDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    //public DbSet<Sales> Sales { get; set; }
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Carpet> Carpets { get; set; } = default!;
    public DbSet<Rug> Rugs { get; set; } = default!;
    public DbSet<RawMaterial> RawMaterials { get; set; } = default!;

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure TPC mapping
        modelBuilder.Entity<Product>().UseTpcMappingStrategy();

        modelBuilder.Entity<Carpet>().ToTable("Carpets");
        modelBuilder.Entity<Rug>().ToTable("Rugs");
        modelBuilder.Entity<RawMaterial>().ToTable("RawMaterials");

        base.OnModelCreating(modelBuilder);
    }

}
