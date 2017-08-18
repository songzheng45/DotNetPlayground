using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Users.Infrastructure;
using Users.Models;

namespace Users.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Users.Infrastructure.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            ContextKey = "Users.Infrastructure.AppIdentityDbContext";

            // register mysql code generator
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Users.Infrastructure.AppIdentityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /**
            * 程序第一次启动时, 初始化管理员信息
            * 由于此时OWIN Configure() 还未执行完, 所以这里直接实例化 RoleManager 和 UserManager 的实例
            * 不要忘记 context
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

            // 接下来使用的都是同步方法(非异步方法), 当需要执行一系列有顺序的操作时, 同步方法很有用.
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

            foreach (AppUser dbUser in userMgr.Users)
            {
                dbUser.City = Cities.北京; ;
            }

            foreach (AppUser dbUser in userMgr.Users)
            {
                if (dbUser.Country == Countries.无)
                {
                    dbUser.Country = dbUser.GetCountryFromCity(dbUser.City);
                }
            }

            context.SaveChanges();
        }
    }
}
