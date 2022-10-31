using System.Configuration;
using Tienda.Models;
using Tienda.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Tienda.Controllers
{
    public class AutenticacionController
    {

    }
    public class AutenticacionControllerConexion:ControllerBase
    {
        private readonly string secretKey;
        
        public AutenticacionControllerConexion(IConfiguration config)
        {
            secretKey = config.GetSection("TokenSettings").GetSection("SecretKey").ToString();
        }
        [HttpPost]
        [Route("api/Validar")]
        public IActionResult ValidarUser([FromBody] UsuarioModel user)
        {
            try
            {
                if (user.correo.Equals("c@correo.com") && user.password.Equals("123"))
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();//Solicitudes de Permisos
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.correo));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string tokenBuilder = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status200OK, new { token = tokenBuilder });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { token = "Usuario no válido para autorizar" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { token = e.Message });
            }
        }
    }
}
