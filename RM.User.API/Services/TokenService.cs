
namespace JwtRoleAuthentication.Services;

using Microsoft.IdentityModel.Tokens;
using RM.User.Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    #region Fields
    // Specify how long until the token expires
    private const int ExpirationMinutes = 30;
    private readonly ILogger<TokenService> _logger;
    #endregion

    #region Constructeur
    public TokenService(ILogger<TokenService> logger)
    {
        _logger = logger;
    }

    #endregion
    public string CreateToken(Utilisateur user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();

        _logger.LogInformation("JWT Token created");

        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["ValidIssuer"],
            new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["ValidAudience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private List<Claim> CreateClaims(Utilisateur user)
    {
        var jwtSub = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["JwtRegisteredClaimNamesSub"];

        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
                new Claim("JwtRegisteredClaimNames.Jti", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("id", user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("role",user.Role.ToString()),
                new Claim("name", user.FirstName+" "+ user.LastName),


            };

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var symmetricSecurityKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["SymmetricSecurityKey"];

        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}