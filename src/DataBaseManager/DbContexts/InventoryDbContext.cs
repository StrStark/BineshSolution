using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Inventory;

namespace DataBaseManager.DbContexts
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Carpet> Carpets { get; set; } = default!;
        public DbSet<Rug> Rugs { get; set; } = default!;
        public DbSet<RawMaterial> RawMaterials { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().UseTpcMappingStrategy();

            modelBuilder.Entity<Carpet>().ToTable("Carpets");
            modelBuilder.Entity<Rug>().ToTable("Rugs");
            modelBuilder.Entity<RawMaterial>().ToTable("RawMaterials");

            modelBuilder.Entity<Inventory>(I =>
            {
                I.HasKey(x => x.Id);
                I.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

                I.Property(e => e.Code).IsRequired();
                I.Property(e => e.Description).HasMaxLength(500);
                I.Property(e => e.Address).HasMaxLength(500);
                I.Property(e => e.Manager).HasMaxLength(200);

                I.HasOne(e=>e.Account)
                 .WithMany()
                 .HasForeignKey(e=>e.AccountId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired(false); // there is a damn problem here !!!!!!!!!!!!!!!!!!!

                I.HasMany(i => i.Products)
                 .WithOne(p=> p.Inventory)
                 .HasForeignKey(p => p .InventoryId)
                 .OnDelete(DeleteBehavior.Restrict);


            });
            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).HasDefaultValueSql("gen_random_uuid()");

                e.Property(p => p.Manufacturer).HasMaxLength(200);
                e.Property(p => p.ProductCode).HasMaxLength(200);
                e.Property(p => p.ProductDesc).HasMaxLength(500);
                e.Property(p => p.ProductDesc2).HasMaxLength(500);
                e.Property(p => p.ProductDescBarcode).HasMaxLength(200);
                e.Property(p => p.ProductDescLatin).HasMaxLength(200);
                e.Property(p => p.ProductIsActive);

                e.HasOne(p => p.Inventory)
                 .WithMany(i => i.Products)
                 .HasForeignKey(p => p.InventoryId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Carpet>(e =>
            {
                e.Property(c => c.ColorPalette).HasMaxLength(200);
                e.Property(c => c.Density).HasMaxLength(100);
                e.Property(c => c.genus).HasMaxLength(100);
                e.Property(c => c.Grade).HasMaxLength(100);
                e.Property(c => c.Color).HasMaxLength(100);
                e.Property(c => c.BorderColor).HasMaxLength(100);
                e.Property(c => c.Size).HasMaxLength(100);
                e.Property(c => c.Shoulder).HasMaxLength(100);
                e.Property(c => c.WeavePattern).HasMaxLength(200);
                e.Property(c => c.DeviceNumber).HasMaxLength(100);
                e.Property(c => c.Buyer).HasMaxLength(200);
                e.Property(c => c.DesignCode).HasMaxLength(100);
                e.Property(c => c.ProjectName).HasMaxLength(200);
                e.Property(c => c.DesignName).HasMaxLength(200);
                e.Property(c => c.WeaveType).HasMaxLength(100);
            });

            modelBuilder.Entity<Rug>(e =>
            {
                e.Property(r => r.Name).HasMaxLength(200);
                e.Property(r => r.WeaveType).HasMaxLength(100);
                e.Property(r => r.Design).HasMaxLength(200);
                e.Property(r => r.Color).HasMaxLength(100);
                e.Property(r => r.Size).HasMaxLength(100);
                e.Property(r => r.Width).HasMaxLength(100);
                e.Property(r => r.Buyer).HasMaxLength(200);
                e.Property(r => r.DesignCode).HasMaxLength(100);
            });

            modelBuilder.Entity<RawMaterial>(e =>
            {
                e.Property(rm => rm.Type).HasMaxLength(100);
                e.Property(rm => rm.Gender).HasMaxLength(100);
                e.Property(rm => rm.PackageType).HasMaxLength(100);
                e.Property(rm => rm.Color).HasMaxLength(100);
                e.Property(rm => rm.Number).HasMaxLength(100);
                e.Property(rm => rm.Serial).HasMaxLength(100);
                e.Property(rm => rm.Code).HasMaxLength(100);
                e.Property(rm => rm.DeviceUsage).HasMaxLength(200);
                e.Property(rm => rm.ProjectName).HasMaxLength(200);
                e.Property(rm => rm.Extra1).HasMaxLength(200);
                e.Property(rm => rm.Extra2).HasMaxLength(200);
                e.Property(rm => rm.Extra3).HasMaxLength(200);
            });
        }
    }
}
