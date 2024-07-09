using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManagementMVC.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration configuration;

    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GetJwtToken(string email, string role)
    {
        var key = configuration.GetSection("Jwt:Key").Value;
        var iss = configuration.GetSection("Jwt:Issuer").Value;
        var aud = configuration.GetSection("Jwt:Audience").Value;

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        var ke = Encoding.UTF8.GetBytes(key);
        var credential = new SigningCredentials(new SymmetricSecurityKey(ke),
                SecurityAlgorithms.HmacSha256);

        var tokendiscriptor = new SecurityTokenDescriptor()
        {
            Subject = GenerateClaims(email, role),
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = credential,
            Audience = aud,
            Issuer = iss
        };
        var token = handler.CreateToken(tokendiscriptor);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(string uname, string role)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, uname));
        claims.Add(new Claim(ClaimTypes.Role, role));
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

        return claimsIdentity;
    }
}
