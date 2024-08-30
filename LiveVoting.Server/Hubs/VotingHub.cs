using Microsoft.AspNetCore.SignalR;

namespace LiveVoting.Server.Hubs;

public class VotingHub : Hub
{
    public async Task UpdateVoteCount(string imageUrl, int voteCount)
    {
        await Clients.All.SendAsync("ReceiveVoteUpdate", imageUrl, voteCount);
    }
}