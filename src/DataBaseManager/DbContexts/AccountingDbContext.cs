using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;


namespace DataBaseManager.DbContexts
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }


        public DbSet<Account> Accounts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(e =>
            {
                e.HasKey(a => a.ID);

                e.Property(a => a.ID)
                     .HasDefaultValueSql("gen_random_uuid()");

                e.Property(a => a.Name).HasMaxLength(200);
                e.Property(a => a.Desc).HasMaxLength(500);
                e.Property(a => a.ArticleDescription).HasMaxLength(500);
                e.Property(a => a.OperationName).HasMaxLength(200);
                e.Property(a => a.chequeCode).HasMaxLength(100);
                e.Property(a => a.SeriesNumber).HasMaxLength(100);

                e.HasIndex(a => a.Name);

                e.HasMany(a => a.SubAccounts)
                 .WithOne(a => a.Parent)
                 .HasForeignKey(a => a.ParentId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
