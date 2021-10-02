using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WeatherSPA.Models;
using System.Threading.Tasks;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

namespace WeatherSPA.Services
{
    public interface IJWTTokenDescriptor
    {
        public Task<string> GetJWTToken(User user);
    }


    public class JWTTokenDescriptor : IJWTTokenDescriptor
    {

        private SignInManager<User> _SignInManager;
        private IConfiguration _Configuration;

        public JWTTokenDescriptor(SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _SignInManager = signInManager;
            _Configuration = configuration;
        }


        public async Task<string> GetJWTToken(User user)
        {

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = (await _SignInManager.CreateUserPrincipalAsync(user))
                              .Identities.First(),

                Claims = new Dictionary<string, object>() { { "Name", user.Name } },

                Expires = DateTime.Now.AddMinutes(int.Parse(
                              _Configuration["BearerTokens:ExpiryMins"])),

                SigningCredentials = new SigningCredentials(
                              new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                  _Configuration["BearerTokens:Key"])),
                                  SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken secToken = new JwtSecurityTokenHandler()
                .CreateToken(descriptor);

            return handler.WriteToken(secToken);
        }
    }
}
