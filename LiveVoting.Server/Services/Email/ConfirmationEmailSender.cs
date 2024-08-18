namespace LiveVoting.Server.Services.Email;

public class ConfirmationEmailSender : IConfirmationEmailSender
{
    public async Task SendAccountConfirmationEmail(string to)
    {
        try
        {
            // var client = new SendGridClient(apiKey);
            // var from = new EmailAddress("razvanboita1609@gmail.com", "Election.ro");
            // var subject = "Confirm your account on ElectiOnline";
            // var toAddress = new EmailAddress(to, "Exemplu");
            // var plainTextContent = "and easy to do anywhere, even with C#";
            // var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            // var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, plainTextContent, htmlContent);
            // var response = await client.SendEmailAsync(msg);
            // Console.WriteLine(response.StatusCode);
            // Console.WriteLine(await response.Body.ReadAsStringAsync());
            Console.WriteLine("Sending account confirmation email...");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}