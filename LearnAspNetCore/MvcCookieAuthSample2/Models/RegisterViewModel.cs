using System.ComponentModel.DataAnnotations;

namespace MvcCookieAuthSample2.Models
{
    public class RegisterViewModel
    {
        [Required]
        //[Required(DataType.EmailAddress)]
        public string Email
        {
            get;
            set;
        }

        [Required]
        public string Password
        {
            get;
            set;
        }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "两次密码不相同")]
        public string ConfirmedPassword
        {
            get;
            set;
        }
    }
}