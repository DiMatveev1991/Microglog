using BdService.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BdService.DAL
{
    public class BlogDbContext : IdentityDbContext<User>
    {
        /// Ссылка на таблицу Posts
        public DbSet<Post>? Posts { get; set; }
        /// Ссылка на таблицу Tags
        public DbSet<Tag>? Tags { get; set; }
        /// Ссылка на таблицу Comments
        public DbSet<Comment>? Comments { get; set; }

        public DbSet<User>? Users { get; set; }


        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
          {
              Database.EnsureCreated();
          }
 
        
    }
}