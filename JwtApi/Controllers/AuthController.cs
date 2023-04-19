using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login/{id}")]
        public ActionResult Login(int id)
        {
            string jwt = CreateToken(id);
            return Ok(jwt);
        }

        private string CreateToken(int id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yuuwameiwaniuuwa"));

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

}
