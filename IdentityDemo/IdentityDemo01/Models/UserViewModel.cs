using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Users.Models
{
    public class CreateModel
    {
        [DisplayName("用户名")]
        [Required]
        public string Name { get; set; }

        [DisplayName("邮箱")]
        [Required]
        public string Email { get; set; }

        [DisplayName("密码")]
        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [DisplayName("用户名")]
        [Required]
        public string Name { get; set; }

        [DisplayName("密码")]
        [Required]
        public string Password { get; set; }
    }
}