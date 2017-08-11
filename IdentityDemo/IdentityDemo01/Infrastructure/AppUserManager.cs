using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Models;

namespace Users.Infrastructure
{
    /// <summary>
    /// UserManager 类是 Identity 诸多类中最重要的类之一, 它用来管理用户类的实例
    /// 必须继承自 UserManager<T>, 而 T 就是用户类
    /// UserManager<T> 并不特定于 Entity Framework, 而是提供更通用的特性来创建和操作用户的数据
    /// </summary>
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        // 当我们需要操作用户数据时, Identity 需要一个 AppUserManager 实例时, 然后就会调用 Create 方法
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

            manager.PasswordValidator = new CustomPasswordValidator()
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonLetterOrDigit = false,
                RequireUppercase = true
            };

            manager.UserValidator = new CustomUserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = true,  // 是否允许用户名中只包含字母数字
                RequireUniqueEmail = true   // 是否要求Email在系统中唯一
            };

            return manager;
        }
    }
}