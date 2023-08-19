namespace JwtProducer.Builder;

public interface IJwtBuilder
{
    TokenResult GenerateAccessToken(TokenRequest tokenRequest, ExpireType expireType, int ExpireTime);
    TokenResult GenerateRefreshToken(ExpireType expireType, int ExpireTime);
}