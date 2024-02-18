using Microsoft.OpenApi.Models;

namespace WebAPI.Capabilities;

public static class StartupSwagger
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            var basePath = Directory.GetCurrentDirectory();
            var xmlFiles = Directory.EnumerateFiles(basePath, "*.xml", SearchOption.AllDirectories);
            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile);
            }            
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        });

        return app;
    }
}
