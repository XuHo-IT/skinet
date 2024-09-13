using API.Errors;
using API.Helpers;
using API.Middleware;
using API.Extensions;  // Add this
using Core.Interface;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services config to the container.
var config = builder.Configuration;
builder.Services.AddDbContext<StoreContext>(
    x => x.UseSqlite(config.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Add application-specific services from the extension
builder.Services.AddApplicationServices(); // This line application

builder.Services.AddSwaggerDocumentation(); // This line swagger

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Migrate the database on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseSwaggerDocumentaion();

app.UseAuthorization();

app.MapControllers();

app.Run();
