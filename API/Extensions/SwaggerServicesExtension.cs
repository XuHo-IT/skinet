using System;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class SwaggerServicesExtension
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Skinet API", Version = "v1" });
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentaion(this IApplicationBuilder app){
        app.UseSwagger();

app.UseSwaggerUI(c 
    => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skinet API v1"); });
    return app;
    }
}
