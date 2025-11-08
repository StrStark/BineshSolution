
using BineshSoloution.Models.AuthModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BineshSoloution.DbContexts;

public class ApplicationIdentityDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Token>()
            .HasKey(t => t.Id);
        builder.Entity<UserSession>()
            .HasKey(x => x.SessionUniqueId);

        builder.Entity<User>()
             .HasMany(u => u.Sessions)
             .WithOne(s => s.User)
             .HasForeignKey(s => s.UserId)
             .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<UserSession>()
            .HasOne(s => s.Token)
            .WithOne(t => t.UserSession)
            .HasForeignKey<Token>(s => s.UserSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
