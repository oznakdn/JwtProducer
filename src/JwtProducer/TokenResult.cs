namespace JwtProducer;

public record TokenResult(string? Token, DateTime? ExpireDate);
