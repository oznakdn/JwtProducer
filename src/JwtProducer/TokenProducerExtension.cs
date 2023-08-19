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
            options.SaveToken = configuration.GetValue<bool>("JwtOptions:SaveToken");
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = configuration.GetValue<bool>("JwtOptions:ValidateIssuer"),
                ValidateAudience = configuration.GetValue<bool>("JwtOptions:ValidateAudience"),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = configuration.GetValue<bool>("JwtOptions:ValidateLifetime"),
                ValidIssuer = configuration.GetValue<bool>("JwtOptions:ValidateIssuer") == true ? configuration.GetValue<string>("JwtOptions:Isser") : null,
                ValidAudience = configuration.GetValue<bool>("JwtOptions:ValidateAudience") == true ? configuration.GetValue<string>("JwtOptions:Audience") : null,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtOptions:SigningKey")!)),
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
        
    }
}