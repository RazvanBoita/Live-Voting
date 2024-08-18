using LiveVoting.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveVoting.Server.Data;

public class VotingDbContext : DbContext
{
    public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}