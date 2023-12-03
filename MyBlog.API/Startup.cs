using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBlog.API.Extentions;
using MyBlog.BLL.Validators.Users;
using MyBlog.Data;
using MyBlog.Data.Models.Roles;
using MyBlog.Data.Models.Users;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyBlog.API
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
			services.AddControllers(options =>
			{
				options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
				options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
				{
					ReferenceHandler = ReferenceHandler.Preserve,
				}));
			});
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "MyBlog API",
					Description = "Application program interface for MyBlog",
				});
			});

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

			services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
				.AddCookie("Cookies", options =>
				{
					options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
					{
						OnRedirectToLogin = redirectContext =>
						{
							redirectContext.HttpContext.Response.StatusCode = 401;
							return Task.CompletedTask;
						}
					};
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlog v1"));
			}
			app.UseRouting();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
