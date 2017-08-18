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
        : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }
}