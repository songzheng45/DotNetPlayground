using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveControllerFromDI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HomeController>(it => new HomeController { Desc = "Create Instance From DI" });

            //
            // 虽然
            // 默认情况下，MvcMiddleware 使用 IControllerActivator 创建 Controller 的实例，而不是从 DI 中获取实例。
            //
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                //.AddControllersAsServices();  // 实现从DI中实例化Controller
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
