using System.ComponentModel.DataAnnotations;

namespace JwtAuthSample
{
    public class LoginViewModel
    {
        [Display( Name = "uname")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "pwd")]
        [Required]
        public string Password { get; set; }
    }
}