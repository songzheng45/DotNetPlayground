using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Models;
using System.Threading.Tasks;

namespace Users.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager mgr)
            : base(mgr)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            if (!user.Email.ToUpper().Contains("@EXAMPLE.COM"))
            {
                var errors = result.Errors.ToList();
                errors.Add("只允许使用 example.com 地址的邮箱");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}