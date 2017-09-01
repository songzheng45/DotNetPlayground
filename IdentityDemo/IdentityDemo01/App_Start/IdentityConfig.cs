using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Users.Infrastructure;

namespace Users
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            // 说明: CreatePerOwinContext() 方法为每一个请求创建新的 AppIdentityDbContext 和 AppUserManager实例
            app.CreatePerOwinContext(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            // 告诉 Identity 如何使用cookie去标识已验证过的用户
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                // 验证类型.
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

                // 如果没有通过验证则跳转到登录页
                LoginPath = new PathString("/Account/Login")
            });

            // 继承Google账号验证
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGoogleAuthentication(
                clientId: "69798440560-rqrq66cepdco6g6po3tp2cgg0l09ahrk.apps.googleusercontent.com",
                clientSecret: "qY5AW6L1eoDefGq69pz8I75G");
        }
    }
}