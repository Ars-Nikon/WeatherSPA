using System.ComponentModel.DataAnnotations;


namespace WeatherSPA.Models
{
    public class SignInCredentials
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
