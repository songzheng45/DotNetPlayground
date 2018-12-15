using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Author = EFCoreSample.Models.Author;
using Blog = EFCoreSample.Models.Blog;
using BlogDbContext = EFCoreSample.Models.BlogDbContext;

namespace EFCoreSample
{
    internal class Program
    {
        private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[]
        {
            new ConsoleLoggerProvider((cat, level) => level >= LogLevel.Debug, true)
        });

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("settings.json");

            var config = builder.Build();

            var logger = LoggerFactory.CreateLogger(nameof(Program));

            using (var dbContext = new BlogDbContext(config))
            {
                dbContext.Authors.RemoveRange(dbContext.Authors);

                Add(dbContext);

                dbContext.SaveChanges();

                var blogs = dbContext.Blogs;
                foreach (var blog in blogs)
                {
                    logger.LogInformation($"{blog.Author.UserName} - {blog.Title} - {blog.PostDate}");
                }
            }

            Console.WriteLine("Finish!");
        }

        private static void Add(BlogDbContext context)
        {
            var blog1 = new Blog
            {
                Title = "First EF Core Demo",
                PostDate = DateTime.Now
            };
            var blog2 = new Blog
            {
                Title = "Second EF Core Sample",
                PostDate = DateTime.Now.AddDays(2)
            };

            var author = new Author
            {
                UserName = "RunningMan",
                ProfilePhoto = "http://www.google.com"
            };
            author.Blogs.Add(blog1);
            author.Blogs.Add(blog2);

            context.Authors.Add(author);
        }
    }
}