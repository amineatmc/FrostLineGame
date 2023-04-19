using FrostLineGame.Models;
using FrostLineGame.Models.Dto;
using FrostLineGame.Utils;
using FrostLineGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace FrostLineGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _configuration;

        public LoginController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration=configuration;

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<string>> LoginUser(SignIn signIn)
        {
            try
            {
                User user = new User();

                if (string.IsNullOrEmpty(signIn.username))
                {
                    return BadRequest("Mail or Password is invalid");
                }
                else if (string.IsNullOrEmpty(signIn.password))
                {
                    return BadRequest("Mail or Password is invalid");
                }
           
                user = _context.Users.Where(c => c.MailAdress == signIn.username && c.Password == signIn.password).FirstOrDefault();


                JwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator(_configuration);
                string token = jwtTokenGenerator.Generate(user.UserID, user.Name, user.MailAdress,user.UserType);
                return Ok(token);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
