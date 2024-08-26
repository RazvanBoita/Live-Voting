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
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Election> Elections { get; set; }
    public DbSet<ElectionRound> ElectionRounds { get; set; }
    public DbSet<RoundCandidate> RoundCandidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.LoggedUser)
            .WithOne(lu => lu.User)
            .HasForeignKey<LoggedUser>(u => u.LoggedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Election to ElectionRound One-to-many
        modelBuilder.Entity<Election>()
            .HasMany(e => e.ElectionRounds)
            .WithOne(er => er.Election)
            .HasForeignKey(er => er.ElectionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //ElectionRound to RoundCandidate One-to-many
        modelBuilder.Entity<ElectionRound>()
            .HasMany(er => er.Candidates)
            .WithOne(cr => cr.ElectionRound)
            .HasForeignKey(cr => cr.ElectionRoundId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //RoundCandidate to Candidate Many-to-one
        modelBuilder.Entity<RoundCandidate>()
            .HasOne(rc => rc.Candidate)
            .WithMany()
            .HasForeignKey(rc => rc.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}