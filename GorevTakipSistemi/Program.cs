using Entities;
using GorevTakipSistemi.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using NLog;
using Services.Contracts;
var builder = WebApplication.CreateBuilder(args);


LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.DIManager();
builder.Services.ConfigureIdentity();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

var logger=app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
