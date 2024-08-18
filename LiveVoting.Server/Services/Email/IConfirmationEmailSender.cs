namespace LiveVoting.Server.Services.Email;

public interface IConfirmationEmailSender
{
    public Task SendAccountConfirmationEmail(string to);
}