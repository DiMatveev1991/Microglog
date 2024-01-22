using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BdService.BLL.Services;
using BdService.BLL.Services.IServices;
using BdService.DAL;
using BdService.DAL.Models;
using BdService.DAL.Repositories;
using BdService.DAL.Repositories.IRepositories;
using Mapping;

namespace Microblog
{
    public class Program
    {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
                
                builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("BdService.DAL")))
                    .AddIdentity<User, Role>(opts =>
                    {
                        opts.Password.RequiredLength = 5;
                        opts.Password.RequireNonAlphanumeric = false;
                        opts.Password.RequireLowercase = false;
                        opts.Password.RequireUppercase = false;
                        opts.Password.RequireDigit = false;
                        opts.User.RequireUniqueEmail = true;
                    })
                    .AddEntityFrameworkStores<BlogDbContext>();

                var mapperConfig = new MapperConfiguration((v) =>
                {
                    v.AddProfile(new MappingProfile());
                });

                IMapper mapper = mapperConfig.CreateMapper();

                builder.Services.AddSingleton(mapper)
                    .AddTransient<ICommentRepository, CommentRepository>()
                    .AddTransient<IPostRepository, PostRepository>()
                    .AddTransient<ITagRepository, TagRepository>()
                    .AddTransient<IAccountService, AccountService>()
                    .AddTransient<ICommentService, CommentService>()
                    .AddTransient<IHomeService, HomeService>()
                    .AddTransient<IPostService, PostService>()
                    .AddTransient<IRoleService, RoleService>()
                    .AddTransient<ITagService, TagService>()
                    .AddControllersWithViews();

              
                builder.Logging.ClearProviders()
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole();

                var app = builder.Build();

            
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
    }
}

