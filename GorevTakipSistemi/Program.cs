using Entities;
using GorevTakipSistemi.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using NLog;
using Services.Contracts;
using Microsoft.OpenApi.Models;
using GorevTakipSistemi;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8082", "https://localhost:7201")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Bearer token'ýnýzý girin: Bearer {token}"
    });

   


    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }});
});


builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.DIManager();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureJWT(builder.Configuration);
var app = builder.Build();
app.UseCors("AllowFrontend");


var logger =app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);
app.UseHttpsRedirection();

// Saldýralara yönelik koruma saðlar.
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    //context.Response.Headers.Add("Content-Security-Policy",
    //"default-src 'self'; script-src 'self' https://cdnjs.cloudflare.com; style-src 'self' https://fonts.googleapis.com https://cdnjs.cloudflare.com;");

    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();

}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();





