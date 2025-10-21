using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;


namespace DataBaseManager.DbContexts;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

    public DbSet<Sales> Sales { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("Products");
            e.HasKey(p => p.Id);
            e.Metadata.SetIsTableExcludedFromMigrations(true); // 👈 crucial
        });

        modelBuilder.Entity<Sales>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Id).HasDefaultValueSql("gen_random_uuid()");

            e.Property(s => s.Date).IsRequired();
            e.Property(s => s.Incoming);
            e.Property(s => s.Outgoing);
            e.Property(s => s.DeliveredQuantity);
            e.Property(s => s.RequestNumber);
            e.Property(s => s.State).HasConversion<string>();

            
            e.HasOne(s => s.Invoice)
             .WithOne()
             .HasForeignKey<Sales>(s => s.InvoiceId)
             .OnDelete(DeleteBehavior.Restrict);

            
            e.HasOne(s => s.Price)
             .WithOne()
             .HasForeignKey<Sales>(s => s.PriceId)
             .OnDelete(DeleteBehavior.Restrict);

            
            e.HasOne(s => s.Product)
             .WithMany()
             .HasForeignKey(s => s.ProductId)
             .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Invoice>(e =>
        {
            e.HasKey(i => i.Id);
            e.Property(i => i.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(i => i.Type).HasConversion<string>();
            e.Property(i => i.Request);
            e.Property(i => i.invoice);
            e.Property(i => i.DocNumber);
            e.Property(i => i.Counterparty).HasMaxLength(200);
        });

        
        modelBuilder.Entity<Price>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(p => p.Fee);
            e.Property(p => p.Receipt);
            e.Property(p => p.Voucher);
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
        });
    }
}
