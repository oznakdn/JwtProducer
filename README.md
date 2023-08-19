<h1>JwtProducer</h1>

<h1>Easy jwt implementation</h1> 

Current Packages

[![Nuget version](https://img.shields.io/nuget/v/JwtProducer.svg?logo=nuget)](https://www.nuget.org/packages/JwtProducer/)
[![Nuget downloads](https://img.shields.io/nuget/dt/JwtProducer?logo=nuget)](https://www.nuget.org/packages/JwtProducer/)
![Build & Test Main](https://github.com/Blazored/LocalStorage/workflows/Build%20&%20Test%20Main/badge.svg)


# $\textcolor{blue}{\textsf{HOW TO USE}}$ 


### $\textcolor{green}{\textsf{Copy to appsettings.json and configure the parameters}}$ 

```
"JwtOption": {
    "SaveToken": true,
    "ValidateIssuer": false,
    "ValidateAudience": false,
    "ValidateLifetime": true,
    "Issuer": null,
    "Audience": null,
    "SigningKey": "You should be write here your security key!"
  }
```
### $\textcolor{green}{\textsf{Add to program.cs}}$ 

```
builder.Services.AddJwtProducer(builder.Configuration);
```

```
app.UseAuthentication();
app.UseAuthorization();
```

### $\textcolor{green}{\textsf{Use in controller}}$ 
```
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtBuilder _jwtBuilder;
        public AuthController(AppDbContext dbContext, IJwtBuilder jwtBuilder)
        {
            _jwtBuilder = jwtBuilder;
        }
    }
```



