using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace EFCoreSample.Models
{
    public class BlogDbContext : DbContext
    {
        public static readonly LoggerFactory EfLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public DbSet<EFCoreSample.Models.Author> Authors { get; set; }
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
                .UseMySql("Server=localhost;Database=blog;User=root;Password=123456;", mysqlOptions =>
            {
                mysqlOptions
                    .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                    .AnsiCharSet(CharSet.Latin1)
                    .UnicodeCharSet(CharSet.Utf8mb4)
                    .MigrationsAssembly("EFCoreSample.Migrations");     // 指定 Migrations 所在的程序集
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(new Author()
            {
                Id = 100,
                UserName = "Joe",
                ProfilePhoto = ""
            });

            modelBuilder.Entity<Blog>().HasData(new Blog()
            {
                Id = 100,
                AuthorId = 100,
                PostDate = DateTime.Now,
                Title = "HOw to drive care",
                UpdateTime = DateTime.Now
            });

            modelBuilder.Entity<Blog>().HasData(new Blog()
            {
                Id = 101,
                AuthorId = 100,
                PostDate = DateTime.Now,
                Title = "Let's go to Thailand",
                UpdateTime = DateTime.Now
            });

        }
    }
}
