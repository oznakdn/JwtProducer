using Example.WebApi.Data;
using Example.WebApi.Dtos;
using JwtProducer;
using JwtProducer.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IJwtBuilder _jwtBuilder;

        public AuthController(AppDbContext dbContext, IJwtBuilder jwtBuilder)
        {
            _dbContext = dbContext;
            _jwtBuilder = jwtBuilder;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _dbContext.Users
                .Include(user => user.Role)
                .SingleOrDefaultAsync(user => user.Email == loginDto.Email && user.Password == loginDto.Password);

            if (user == null)
            {
                return NotFound("Email or password is wrong!");
            }

            var acccesstoken = _jwtBuilder.GenerateAccessToken(new TokenRequest(user.Email, user.Role.RoleName, null, null, null), ExpireType.Minute, 10);
            var refreshToken = _jwtBuilder.GenerateRefreshToken(ExpireType.Minute, 15);
            if (acccesstoken.ExpireDate >= refreshToken.ExpireDate)
            {
                return BadRequest("Access token's expire time should not be greather than refresh token's expire time");
            }
            return Ok(new { Token = acccesstoken.Token, AccessExpire = acccesstoken.ExpireDate, Refresh = refreshToken.Token, RefreshExpire = refreshToken.ExpireDate });

        }
    }
}
