using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
namespace GorevTakipSistemi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        
        =>services.AddDbContext<GTSDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        
        public static void DIManager(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddSingleton<ILoggerService, LoggerManager>();
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IServiceManager,ServiceManager>();
            services.AddScoped<IAuthenticationService,AuthenticationManager>();
            services.AddScoped<ICompanyService,CompanyManager>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<IReportService, ReportManager>();
            services.AddScoped<IReportRepository, ReportRepository>();
            
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = true;
                opts.User.RequireUniqueEmail=true;
           
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<GTSDbContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)           
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                RoleClaimType = ClaimTypes.Role,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            }
            );


        }

       

    }
}
