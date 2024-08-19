using LiveVoting.Server.Services.Jwt;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LiveVoting.Server.Services.Email;

public class ConfirmationEmailSender : IConfirmationEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;

    public ConfirmationEmailSender(IConfiguration configuration, IJwtService jwtService)
    {
        _configuration = configuration;
        _jwtService = jwtService;
    }
    public async Task SendAccountConfirmationEmail(string to)
    {
        try
        {
            var apiKey = _configuration["SendgridApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("razvanboita1609@gmail.com", "Election.ro");
            var subject = "Confirm your account on ElectiOnline";
            var toAddress = new EmailAddress(to, "Exemplu");

            var token = _jwtService.GenerateTokenWithEncodedEmail(to);
            var confirmationLink = _configuration["Server"] + "api/verify/" + "?token=" + token;
            var plainTextContent = "Click the link to verify your account: {confirmationLink}"; 
            var htmlContent = $"<strong>Click the link to verify your account: {confirmationLink}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
            Console.WriteLine("Email sender says: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}