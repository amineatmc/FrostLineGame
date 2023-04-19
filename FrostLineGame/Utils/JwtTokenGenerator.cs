using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FrostLineGame.Utils
{
    public class JwtTokenGenerator
    {
        public IConfiguration _configuration;

        private readonly TimeSpan _timeSpan;
        public JwtTokenGenerator(IConfiguration configuration) : this(TimeSpan.FromHours(1), configuration)
        {
        }

        public JwtTokenGenerator(TimeSpan timeSpan, IConfiguration configuration)
        {
            _timeSpan = timeSpan;
            _configuration = configuration;
        }


        private string Generator(int id, string userName, string mail, string issuer, string audience, byte[] key,string userType)
        {

        
            var securityDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.Add(_timeSpan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),

                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    //TODO Implemented Further. 
                   new Claim("GuidId", Guid.NewGuid().ToString()),
                   new Claim("id",id.ToString()),
                   new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, userName),
                   new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email,mail),
                   new Claim("user_type",userType.ToString()),
                   new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                })
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public string Generate(int id, string userName, string mail,string userType)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            return Generator(id, userName, mail, _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], key,userType);
        }
    }
}
