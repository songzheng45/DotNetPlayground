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
            * �����һ������ʱ, ��ʼ������Ա��Ϣ
            * ���ڴ�ʱOWIN Configure() ��δִ����, ��������ֱ��ʵ���� RoleManager �� UserManager ��ʵ��
            * ��Ҫ���� context
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

            // ������ʹ�õĶ���ͬ������(���첽����), ����Ҫִ��һϵ����˳��Ĳ���ʱ, ͬ������������.
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
                dbUser.City = Cities.����; ;
            }

            foreach (AppUser dbUser in userMgr.Users)
            {
                if (dbUser.Country == Countries.��)
                {
                    dbUser.Country = dbUser.SetCountryFromCity(dbUser.City);
                }
            }

            context.SaveChanges();
        }
    }
}
