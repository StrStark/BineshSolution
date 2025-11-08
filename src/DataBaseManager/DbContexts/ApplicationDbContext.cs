using BineshSoloution.Models.AuthModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BineshSoloution.Models.DataBaseModels.Costumers;
using BineshSoloution.Models.DataBaseModels.Inventory;
using BineshSoloution.Models.DataBaseModels.Sales;
using BineshSoloution.Models.Sales;
using BineshSoloution.Models.Account;
using BineshSoloution.Models.Inventory;


namespace BineshSoloution.DbContexts;

public class ApplicationDbContext :  DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; } = default!;
    
    public DbSet<Sales> Sales { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Carpet> Carpets { get; set; } = default!;
    public DbSet<Rug> Rugs { get; set; } = default!;
    public DbSet<RawMaterial> RawMaterials { get; set; } = default!;
    public DbSet<Receipt> Receipts { get; set; }

    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<Region> Regions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(e =>
        {
            e.HasKey(a => a.ID);

            e.Property(a => a.ID)
                 .HasDefaultValueSql("gen_random_uuid()");

            e.Property(a => a.Name).HasMaxLength(200);
            e.Property(a => a.ArticleDescription).HasMaxLength(500);
            e.Property(a => a.OperationName).HasMaxLength(200);
            e.Property(a => a.chequeCode).HasMaxLength(100);
            e.Property(a => a.SeriesNumber).HasMaxLength(100);

            e.HasIndex(a => a.Name);

            e.HasMany(a => a.SubAccounts)
             .WithOne(a => a.Parent)
             .HasForeignKey(a => a.ParentId)
             .OnDelete(DeleteBehavior.Cascade);

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

            e.HasOne(c => c.Counterparty)
            .WithMany()
            .HasForeignKey(c => c.CounterpartyId)
            .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Price>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(p => p.Fee);
            e.Property(p => p.Receipt);
            e.Property(p => p.Voucher);
        });

        modelBuilder.Entity<Inventory>(I =>
        {
            I.HasKey(x => x.Id);
            I.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

            I.Property(e => e.Code).IsRequired();
            I.Property(e => e.Description).HasMaxLength(500);
            I.Property(e => e.Address).HasMaxLength(500);
            I.Property(e => e.Manager).HasMaxLength(200);

            I.HasOne(e => e.Account)
             .WithMany()
             .HasForeignKey(e => e.AccountId)
             .OnDelete(DeleteBehavior.Cascade);

            I.HasMany(i => i.Products)
             .WithOne(p => p.Inventory)
             .HasForeignKey(p => p.InventoryId)
             .OnDelete(DeleteBehavior.Restrict);

        });
        modelBuilder.Entity<Product>().UseTpcMappingStrategy();
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
        modelBuilder.Entity<Carpet>().ToTable("Carpets");
        modelBuilder.Entity<Rug>().ToTable("Rugs");
        modelBuilder.Entity<RawMaterial>().ToTable("RawMaterials");
        modelBuilder.Entity<Receipt>(e =>
        {
            e.ToTable("Receipts");

            e.HasKey(r => r.Id);
            e.HasIndex(r => r.Id);
            e.Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(r => r.ProductCode).IsRequired();
            e.Property(r => r.Date).IsRequired();
            e.Property(r => r.Descryption).HasMaxLength(500);
            e.Property(r => r.ReceiptType).HasConversion<string>(); // store enum as string.IsRequired().HasMaxLength(50);
            e.Property(r => r.ReceiptType).HasMaxLength(200);
            e.Property(r => r.Sender).HasMaxLength(200);
            e.Property(r => r.Status).HasConversion<string>(); // store enum as string.IsRequired().HasMaxLength(50);
            e.Property(r => r.Price).HasColumnType("bigint").IsRequired();
            e.Property(r => r.CreatedBy).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

            e.Property(c => c.Active).IsRequired();
            e.Property(c => c.Type).HasConversion<string>().IsRequired();
            e.Property(c => c.Desc);
            e.Property(c => c.PaymentReliability).IsRequired();

            e.HasOne(c => c.Person)
             .WithMany()
             .HasForeignKey(c => c.PersonId)
             .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Person>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");

            e.Property(p => p.Name).HasMaxLength(200);
            e.Property(p => p.Family).HasMaxLength(200);
            e.Property(p => p.Phone).HasMaxLength(50);
            e.Property(p => p.Fax).HasMaxLength(50);
            e.Property(p => p.PhoneNumber).HasMaxLength(50);

            e.HasOne(p => p.Region)
             .WithMany()
             .HasForeignKey(p => p.RegionId)
             .OnDelete(DeleteBehavior.Restrict);

        });
        modelBuilder.Entity<Region>(e =>
        {
            e.ToTable("Regions");
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");

            e.Property(r => r.Country).HasMaxLength(500);
            e.Property(r => r.City).HasMaxLength(100);
            e.Property(r => r.CityRegion).HasMaxLength(100);
            e.Property(r => r.Mahale).HasMaxLength(100);
        });


        modelBuilder.Entity<Token>()
                .HasKey(t => t.Id);
        modelBuilder.Entity<UserSession>()
            .HasKey(x => x.SessionUniqueId);

        modelBuilder.Entity<User>()
             .HasMany(u => u.Sessions)
             .WithOne(s => s.User)
             .HasForeignKey(s => s.UserId)
             .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<UserSession>()
            .HasOne(s => s.Token)
            .WithOne(t => t.UserSession)
            .HasForeignKey<Token>(s => s.UserSessionId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
