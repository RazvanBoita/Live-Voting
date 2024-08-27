namespace LiveVoting.Server.Repositories.Election;

public interface IElectionRepository
{
    public Models.Election? GetElectionByYear(int year);
    public Models.Election? GetElectionById(int id);
}