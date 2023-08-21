<h1>JwtProducer</h1>

<h1>Easy jwt implementation</h1> 

Current Packages

[![Nuget version](https://img.shields.io/nuget/v/JwtProducer.svg?logo=nuget)](https://www.nuget.org/packages/JwtProducer/)
[![Nuget downloads](https://img.shields.io/nuget/dt/JwtProducer?logo=nuget)](https://www.nuget.org/packages/JwtProducer/)
![Build & Test Main](https://github.com/Blazored/LocalStorage/workflows/Build%20&%20Test%20Main/badge.svg)


<h2>HOW TO USE</h2>


<h4>Copy to appsettings.json and configure the parameters</h4>

```
"JwtOption": {
    "SaveToken": true, // true or false
    "ValidateIssuer": false, // true or false
    "ValidateAudience": false, // true or false
    "ValidateLifetime": true, // true or false
    "Issuer": null, // string
    "Audience": null, // string
    "SigningKey": "You should be write here your security key!" // string
  }
```
<h4>Add to program.cs</h4>

```
builder.Services.AddJwtProducer(builder.Configuration);
```

```
app.UseAuthentication();
app.UseAuthorization();
```

<h4>Usage</h4>

```
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtBuilder _jwtBuilder;
        public AuthController(IJwtBuilder jwtBuilder)
        {
            _jwtBuilder = jwtBuilder;
        }
    }
```



