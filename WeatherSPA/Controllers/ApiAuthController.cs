using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WeatherSPA.Models;
using WeatherSPA.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WeatherSPA.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class ApiAuthController : ControllerBase
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private IConfiguration _Configuration;
        private IJWTTokenDescriptor _JWTTokenDescriptor;

        public ApiAuthController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IConfiguration configuration,
            IJWTTokenDescriptor jWTToken)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _Configuration = configuration;
            _JWTTokenDescriptor = jWTToken;
        }



        [HttpPost("signin")]
        public async Task<IActionResult> ApiSignIn(
                 [FromBody] SignInCredentials creds)
        {
            var user = await _UserManager.FindByEmailAsync(creds.Email);

            if (user == null)
            {
                return BadRequest(new { success = false, ErrorMasege = "Invalid username and / or password" });
            }

            var result = await _SignInManager.CheckPasswordSignInAsync(user,
                creds.Password, true);

            if (result.Succeeded)
            {
                return Ok(new { success = true, token = await _JWTTokenDescriptor.GetJWTToken(user) });
            }

            return BadRequest(new { success = false, ErrorMasege= "Invalid username and / or password" });
        }


        [HttpPost("registration")]
        public async Task<IActionResult> Registration(
             [FromBody] RegistrationDetails registration)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = registration.Email, UserName = registration.Email, Name = registration.Name };

                var result = await _UserManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    return Ok(new { success = true, token = await _JWTTokenDescriptor.GetJWTToken(user) });
                }
                else
                {
                    return BadRequest(new { Errors = result.Errors });
                }
            }
            return BadRequest();
        }

        [HttpPost("Test")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Test()
        {
            return Ok(User.Identity.Name);
        }
    }
}
