﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Users.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Users
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            // CreatePerOwinContext() 方法为每一个请求创建新的 AppIdentityDbContext 和 AppUserManager实例
            app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            // 告诉 Identity 如何使用cookie去标识已验证过的用户
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                // 登录地址, 如果没有通过验证则跳转到这里
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
