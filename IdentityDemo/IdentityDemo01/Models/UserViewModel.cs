using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }

        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}