using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ejercicio6API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthJwtController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthJwtController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Login sencillo para autenticación JWT
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            //credenciales de prueba
            if (request.Username == "admin" && request.Password == "admin")
            {
                //generamos el token jwt
                var token = GenerateJwtToken(request.Username);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Credenciales inválidas");
            }
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)              
            };

            var token = new JwtSecurityToken(
               
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    
    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
