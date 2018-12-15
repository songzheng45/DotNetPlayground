using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace EFCoreSample.DB.Models
{
    public class BlogDbContext : DbContext
    {
        public static readonly LoggerFactory EfLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public BlogDbContext()
        {

        }

        private readonly IConfiguration _config;
        public BlogDbContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(EfLoggerFactory)  // 使用日志提供器
                .EnableSensitiveDataLogging()       // 启用在日志中输出敏感数据
                .UseMySql(_config.GetConnectionString("DefaultConnection"), mysqlOptions =>
            {
                mysqlOptions
                    .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                    .AnsiCharSet(CharSet.Latin1)
                    .UnicodeCharSet(CharSet.Utf8mb4)
                    .MigrationsAssembly("netstandard2.0\\EFCoreSample.Migrations");
            });

            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
