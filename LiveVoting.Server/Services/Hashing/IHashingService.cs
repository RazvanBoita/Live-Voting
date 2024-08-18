namespace LiveVoting.Server.Services.Hashing;

public interface IHashingService
{
    public string Hash(string input);
    public bool Verify(string input, string hash);
}