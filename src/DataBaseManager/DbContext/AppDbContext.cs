using Microsoft.EntityFrameworkCore;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Sales> Sales { get; set; }

}
