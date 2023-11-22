using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ControleDeMateriais.Application.Services.Token;
public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tokenLifetimeInMinutes;
    private readonly string _secretKey;

    public TokenController(
        double tokenLifetimeInMinutes, 
        string secretKey)
    {
        _tokenLifetimeInMinutes = tokenLifetimeInMinutes;
        _secretKey = secretKey;
    }

    public string TokenGenerate(string userEmail)
    {
        var claims = new List<Claim>
        { 
            new Claim(EmailAlias, userEmail),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenLifetimeInMinutes),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        var claims = tokenHandler.ValidateToken(token, validationParameters, out _);
        return claims;
    }

    public string RecoverEmail(string token)
    {
        var claims = ValidateToken(token);
        return claims.FindFirst(EmailAlias).Value;
    }

    private SymmetricSecurityKey SimetricKey()
    {
        var simmetricKey = Convert.FromBase64String(_secretKey);

        return new SymmetricSecurityKey(simmetricKey);
    }
}
