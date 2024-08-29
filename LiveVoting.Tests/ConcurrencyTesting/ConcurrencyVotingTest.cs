using LiveVoting.Server.Data;
using LiveVoting.Server.Models;
using LiveVoting.Server.Repositories.UpcomingVote;
using LiveVoting.Server.Services.Voting;
using Microsoft.EntityFrameworkCore;

namespace LiveVoting.Tests;
using LiveVoting.Server.Services;
public class ConcurrencyVotingTest
{
    [Fact]
    public async Task TestConcurrentVoting()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<VotingDbContext>()
            .UseSqlite("Datasource=:memory:")
            .Options;

        var guid = Guid.NewGuid();
        using (var context = new VotingDbContext(options))
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var tasks = new List<Task>();

            // Act
            for (int i = 0; i < 10; i++) // Reduced concurrency for testing
            {
                tasks.Add(Task.Run(() =>
                {
                    using (var innerContext = new VotingDbContext(options))
                    {
                        var repo = new UpcomingVoteRepository(innerContext);
                        var vote = new UpcomingVote
                        {
                            UserId = guid,
                            UpcomingCandidateId = 2
                        };
                        repo.AddVote(vote);
                    }
                }));
            }

            await Task.WhenAll(tasks);

            // Assert
            var votes = context.UpcomingVotes.Where(v => v.UpcomingCandidateId == 2).ToList();
            Assert.Single(votes); // Ensure exactly one vote exists
            Assert.Equal(guid, votes[0].UserId);
        }
    }
}
