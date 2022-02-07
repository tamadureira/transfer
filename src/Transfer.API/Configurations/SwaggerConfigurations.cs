using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Transfer.API.Configurations
{
    public static class SwaggerConfigurations
    {
        public static IServiceCollection ConfigureSwagger<TStartup>(this IServiceCollection services, string swaggerDocTitle, string swaggerDocVersion)
            where TStartup : class
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer.API", Version = "v1" });
                c.IncludeXmlComments(GetXmlCommentsPath((typeof(TStartup).Assembly.GetName().Name)));
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder applicationBuilder, string swaggerDocTitle, string swaggerDocVersion)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerDocTitle + " " + swaggerDocVersion.ToUpper());
            });

            return applicationBuilder;
        }

        private static string GetXmlCommentsPath(string assemblyName) => $"{AppDomain.CurrentDomain.BaseDirectory}{assemblyName}.xml";
    }
}
