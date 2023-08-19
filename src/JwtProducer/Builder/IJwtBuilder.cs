namespace JwtProducer.Builder;

public interface IJwtBuilder
{
    TokenResult GenerateAccessToken(string userEmail, string? userRole, ExpireType expireType, int ExpireTime);
    TokenResult GenerateRefreshToken(ExpireType expireType, int ExpireTime);
}