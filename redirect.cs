using System;
using Microsoft.IdentityModel.Tokens;

class JwtDecoder
{
    static void Main(string[] args)
    {
        string apiKey = "API_KEY"; // Specify your API key
        string phtoken = "your_jwt_token_here"; // Replace with the JWT token you want to decode

        int jwtResponse = 0;
        string jwtPhone = "";

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(apiKey)),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }
        };

        try
        {
            SecurityToken validatedToken;
            var claimsPrincipal = tokenHandler.ValidateToken(phtoken, validationParameters, out validatedToken);

            jwtResponse = 1; // JWT decoded successfully
            var countryCode = claimsPrincipal.FindFirst("country_code").Value;
            var phoneNo = claimsPrincipal.FindFirst("phone_no").Value;
            jwtPhone = countryCode + phoneNo;
        }
        catch (SecurityTokenExpiredException)
        {
            jwtResponse = 0; // Invalid JWT
        }
        catch (SecurityTokenValidationException)
        {
            jwtResponse = 0; // Invalid JWT
        }

        Console.WriteLine($"JWT Response: {jwtResponse}");
        Console.WriteLine($"JWT Phone: {jwtPhone}");
    }
}
