namespace JwtProducer;

public class JwtOption
{
    public bool SaveToken { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? SigningKey { get; set; }
}