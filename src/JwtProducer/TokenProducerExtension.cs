namespace JwtProducer;

public static class TokenProducerExtension
{
    
    public static IServiceCollection AddJwtProducer(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOption>(configuration.GetSection(nameof(JwtOption)));

        services.AddScoped<IJwtBuilder,JwtBuilder>();

        services.AddAuthentication(scheme =>
        {
            scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = configuration.GetValue<bool>("JwtOption:SaveToken");
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = configuration.GetValue<bool>("JwtOption:ValidateIssuer"),
                ValidateAudience = configuration.GetValue<bool>("JwtOption:ValidateAudience"),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = configuration.GetValue<bool>("JwtOption:ValidateLifetime"),
                ValidIssuer = configuration.GetValue<bool>("JwtOption:ValidateIssuer") == true ? configuration.GetValue<string>("JwtOptions:Isser") : null,
                ValidAudience = configuration.GetValue<bool>("JwtOption:ValidateAudience") == true ? configuration.GetValue<string>("JwtOption:Audience") : null,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtOption:SigningKey")!)),
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
        
    }
}