using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApi.Environments;

namespace TodoApi
{
    public class Startup
    {
        private readonly Configuration configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            configuration = new Configuration();
            builder.Bind(configuration);
        }

        //public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton(configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            Func<string, LogLevel, bool> filter = (name, level) => level >= LogLevel.Error;

            loggerFactory.AddConsole(filter);

            // 现在大多网站静态页面都放在CDN里
            //app.UseStaticFiles();

            // WebAPI的项目里，当访问域名时需要返回默认文档（default.html,default.htm,index.html,index.htm)
            // 如果不设置UseFileServer，显示一个空白页
            app.UseFileServer();

            // 使用 MVC 和 WebAPI
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Heello Woooold!");
            //});
        }
    }
}
