using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionsBindSampl
{
    public class Startup
    {
        private IConfiguration _config { get; set; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 将配置文件映射到实体类
            services.Configure<Settings>(_config);
            services.Configure<DatabaseConfiguration>(_config.GetSection("Database"));
            services.Configure<DatabaseConfiguration>("db", options =>
            {
                options.DatabaseName = "DBName FROM Action";
            });
            services.AddMvc();
            services.AddHttpClient("DS.Lottery.PK10");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime application)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvcWithDefaultRoute();

            application.ApplicationStarted.Register(() =>
            {

            });

            app.UseMvcWithDefaultRoute();

            // app.Run(async (context) =>
            // {
            //     // Bind 方式加载配置
            //     /*
            //      * 
            //     var dbConfig = new DatabaseConfiguration();
            //     _config.Bind("Database", dbConfig);

            //     await context.Response.WriteAsync(dbConfig.ConnectionString);

            //     */
            // });

        }
    }
}
