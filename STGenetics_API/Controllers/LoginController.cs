using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STGenetics_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login(string adminName)
        {
            string jwtToken = string.Empty;

            if (adminName == _configuration.GetSection("AdminName").Value)
            {
                jwtToken = await GenerateJwtToken(adminName);
            }
            else
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            return Ok(new { Token = jwtToken });
        }



        private async Task<string> GenerateJwtToken(string adminName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, adminName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:key").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            await Task.Delay(1);

            return token;
        }


    }
}
