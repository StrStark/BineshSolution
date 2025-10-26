using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Costumers;

namespace DataBaseManager.DbContexts
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Region> Regions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


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
        }
    }
}
