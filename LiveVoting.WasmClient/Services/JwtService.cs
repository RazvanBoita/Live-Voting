using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace LiveVoting.WasmClient.Services;

public class JwtService
{
    public static Guid GetGuidClaimFromToken(string jwtToken)
    {
        if (string.IsNullOrWhiteSpace(jwtToken))
            return Guid.Empty;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(jwtToken);

            if (tokenS?.Claims != null)
            {
                var guidClaim = tokenS.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
                if (guidClaim != null && Guid.TryParse(guidClaim.Value, out var guidValue))
                {
                    return guidValue;
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as per your application's requirements
            Console.WriteLine($"Exception occurred when trying to decode JWT: {ex.Message}");
        }

        return Guid.Empty;
    }
}