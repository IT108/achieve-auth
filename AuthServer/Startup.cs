using AuthServer.Extensions;
using AuthServer.Infrastructure.Data.Identity;
using AuthServer.Infrastructure.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net;



namespace AuthServer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			BuildConfig();
			services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["DB:CONNECTION"]));

			/* We'll play with this down the road... 
				services.AddAuthentication()
				.AddGoogle("Google", options =>
				{
					options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

					options.ClientId = "<insert here>";
					options.ClientSecret = "<insert here>";
				});*/

			services.AddTransient<IProfileService, IdentityClaimsProfileService>();

			services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin",
					builder => builder
						.WithOrigins("http://localhost:4200", "https://my.it108.org")
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials()
					);
			});


			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddTestUsers(TestUsers.Users)
				// this adds the operational data from DB (codes, tokens, consents)
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = builder => builder.UseSqlServer(Configuration["DB:CONNECTION"]);
					// this enables automatic token cleanup. this is optional.
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = 30; // interval in seconds
				})
				//.AddInMemoryPersistedGrants()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients())
				.AddAspNetIdentity<AppUser>();

			var cors = new DefaultCorsPolicyService(new LoggerFactory().CreateLogger<DefaultCorsPolicyService>())
			{
				AllowAll = true
			};

			services.AddSingleton<ICorsPolicyService>(cors);


			services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin()
			   .AllowAnyHeader()
				.AllowAnyMethod()));

			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			}).SetCompatibilityVersion(CompatibilityVersion.Latest);
		}

		private void BuildConfig()
		{
			string db_server = Configuration["DB:SERVER"];
			string db_database = Configuration["DB:DATABASE"];
			string db_username = Configuration["DB:USERNAME"];
			string db_pass = Configuration["DB:PASSWORD"];
			Configuration["DB:CONNECTION"] = $"Server={db_server};Database={db_database};User Id={db_username};Password={db_pass};";

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseCors("AllowSpecificOrigin");
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

					var error = context.Features.Get<IExceptionHandlerFeature>();
					if (error != null)
					{
						context.Response.AddApplicationError(error.Error.Message);
						await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
					}
				});
			});


			var serilog = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.Enrich.FromLogContext()
				.WriteTo.File(@"authserver_log.txt");

			loggerFactory.WithFilter(new FilterLoggerSettings
				{
					{ "IdentityServer4", LogLevel.Debug },
					{ "Microsoft", LogLevel.Warning },
					{ "System", LogLevel.Warning },
				}).AddSerilog(serilog.CreateLogger());

			app.UseStaticFiles();
			app.UseIdentityServer();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
