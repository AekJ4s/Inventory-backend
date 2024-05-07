using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace JwtInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly YourDbContextClassName _db;

        public LoginController(IConfiguration config, YourDbContextClassName db)
        {
            _config = config;
            _db = db;
        }

    
    

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid login data.");
            }

              Login login = new Login
             {
            Username = loginRequest.Username,
            Password = loginRequest.Password,
            };

            

             if (Login.GetByUserDetail(_db,login) != null)
            {

                var result = Login.GetByUserDetail(_db, login);
                // สร้าง Token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Issuer"],
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = credentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                UserWithToken returnItem = new UserWithToken();
                returnItem.Tokens = tokenHandler.WriteToken(token);
                returnItem.User = login;
                returnItem.User.Password = null;
                return Ok(returnItem);
            }
            else
            {
                // ถ้าไม่ถูกต้อง ส่งคำตอบผิดพลาดกลับ
                return NotFound("Invalid username or password.");
            }

            
          

         
        

           
        }
    }
}
