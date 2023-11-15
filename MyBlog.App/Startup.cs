using MyBlog.Data.Models.Roles;
using MyBlog.Data.Models.Users;
using MyBlog.Data;
using Microsoft.EntityFrameworkCore;
using MyBlog.App.Extentions;
using FluentValidation.AspNetCore;
using MyBlog.BLL.Validators.Users;

namespace MyBlog.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EditUserViewModelValidator>());
            services.AddDbContext<BlogDbContext>(option => option.UseSqlServer(connectionString), ServiceLifetime.Scoped)
                .AddUnitOfWork()
				.AddRepositories()
				.AddServicesBL()
                .AddAutoMapper()
                .AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<BlogDbContext>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			if (!env.IsDevelopment())
            {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("/Error/Default?statusCode={0}");

            //app.UseExceptionHandler("/Error/500");
            //app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

		}

    }
}
