using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KnowBooks.Data;
using KnowBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using KnowBooks.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Configuration;
using AspNetCore.ReCaptcha;

namespace KnowBooks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add services to the container.
            builder.Services.AddRazorPages(options =>
            {
                //options.Conventions.AuthorizePage("/Books/Create");
                //options.Conventions.AuthorizeAreaPage("Identity", "/Account/Manage");
                //options.Conventions.AuthorizeFolder("/Books");
            });

            builder.Services.AddDbContext<KnowBooksContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("KnowBooksContext") ?? throw new InvalidOperationException("Connection string 'KnowBooksContext' not found.")));


            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<ApplicationRole>().AddEntityFrameworkStores<KnowBooksContext>();


			builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
                // Confirmed Email
                options.SignIn.RequireConfirmedEmail = true;
            });

            builder.Services.AddAuthorization(options => options.AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa")));


            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<EmailSender>(builder.Configuration);

            builder.Services.AddReCaptcha(builder.Configuration.GetSection("ReCaptcha"));

            builder.Services.AddHostedService<ReturnService>();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }

		//public static IHostBuilder CreateHostBuilder(string[] args) =>
		//Host.CreateDefaultBuilder(args)
		//    .ConfigureWebHostDefaults(webBuilder =>
		//    {
		//        webBuilder.UseStartup<Program>();
		//    })
		//    .ConfigureServices((hostContext, services) =>
		//    {
		//        // ... Other services

		//        // Register your email sender implementation
		//        services.AddSingleton<IEmailSender, EmailSender>();
		//        services.AddDefaultIdentity<ApplicationUser>(options =>
		//        {
		//            options.SignIn.RequireConfirmedAccount = true;
		//            // ... Other options
		//        })
		//        .AddRoles<IdentityRole>()
		//        .AddEntityFrameworkStores<KnowBooksContext>();
		//    });

		public void ConfigureServices(IServiceCollection services)
		{
			// ... (your existing service registrations)
			////roles
			services.AddIdentity<ApplicationUser, ApplicationRole>()
				 .AddDefaultUI()
				 .AddRoles<ApplicationRole>()
				 .AddEntityFrameworkStores<KnowBooksContext>()
				 .AddDefaultTokenProviders();



			// Register your email sender implementation
			services.AddSingleton<IEmailSender, EmailSender>();

			services.AddDefaultIdentity<ApplicationUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				// ... (other options)
			})
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<KnowBooksContext>();
		}
	}
}