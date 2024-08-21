using LiveVoting.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveVoting.Server.Data;

public class VotingDbContext : DbContext
{
    public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<LoggedUser> LoggedUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.LoggedUser)
            .WithOne(lu => lu.User)
            .HasForeignKey<LoggedUser>(u => u.LoggedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}