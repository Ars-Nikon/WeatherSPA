using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using WeatherSPA.Models;

namespace WeatherSPA.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class ApiAuthController : ControllerBase
    {
        private SignInManager<User> _SignInManager;
        private UserManager<User> _UserManager;
        private IConfiguration _Configuration;

        public ApiAuthController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _Configuration = configuration;
        }

        [HttpPost("signin")]
        public async Task ApiSignIn(
            
            )
        {

        }


    }
}
