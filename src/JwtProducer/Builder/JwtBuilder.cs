

namespace JwtProducer.Builder;

public class JwtBuilder : IJwtBuilder
{
     private readonly JwtOption _jwtOption;
    public JwtBuilder(IOptions<JwtOption> jwtOption)
    {
        _jwtOption = jwtOption.Value;
    }


    public TokenResult GenerateAccessToken(string userEmail, string? userRole, ExpireType expireType, int ExpireTime)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SigningKey!));

        SigningCredentials _signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

        DateTime _expires = SetExpireDate(expireType, ExpireTime);

        ClaimsIdentity claimsIdentity = new();
        var claims = AddClaimsToToken(userEmail, userRole);
        claimsIdentity.AddClaims(claims);

        SecurityTokenDescriptor securityTokenDescriptor = new()
        {
            Issuer = _jwtOption.Issuer,
            Audience = _jwtOption.Audience,
            SigningCredentials = _signingCredentials,
            Expires = _expires,
            Subject = claimsIdentity
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var createToken = tokenHandler.CreateToken(securityTokenDescriptor);
        var token = tokenHandler.WriteToken(createToken);
        return new TokenResult(token,_expires);
    }

    public TokenResult GenerateRefreshToken(ExpireType expireType, int ExpireTime)
    {
        DateTime expires = SetExpireDate(expireType, ExpireTime);
        var randomNumber = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(randomNumber);
        string refreshToken = Convert.ToBase64String(randomNumber);
        return new TokenResult(refreshToken,expires);
    }

    private DateTime SetExpireDate(ExpireType expireType, int ExpireTime)
    {
        DateTime expires = expireType switch
        {
            ExpireType.Minute => DateTime.Now.AddMinutes(ExpireTime),
            ExpireType.Hour => DateTime.Now.AddHours(ExpireTime),
            ExpireType.Day => DateTime.Now.AddDays(ExpireTime),
            ExpireType.Month => DateTime.Now.AddMonths(ExpireTime),
            _ => throw new Exception("Expire type not found!")
        };

        return expires;
    }

    private List<Claim> AddClaimsToToken(string userEmail, string? userRole)
    {
        List<Claim> claims = new();
        claims.Add(new Claim(ClaimTypes.Email, userEmail));
        if (string.IsNullOrEmpty(userRole))
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole!));
        }

        return claims;
    }
}