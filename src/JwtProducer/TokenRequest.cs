namespace JwtProducer;

public record TokenRequest(string? userEmail, string? username, string? userRole, DateTime? DateOfBirth, string? mobilePhone);

