using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Users.Models;

namespace Users.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        // 调用基类的构造函数, 并传入 ConnectionString 的 name, 用来连接数据库
        public AppIdentityDbContext() : base("IdentityDb") { }

        static AppIdentityDbContext()
        {
            // 应用程序启动时调初始化数据库
            // 只要使用 EF Code First 创建数据库, 就使用指定的初始化类初始化数据库
            Database.SetInitializer(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    public class IdentityDbInit
        : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            /**
             * 程序第一次启动时, 初始化管理员信息
             * 不要忘记context
             */
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string email = "admin@example.com";
            string password = "123456";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser() { UserName = userName, Email = email }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
        }
    }
}