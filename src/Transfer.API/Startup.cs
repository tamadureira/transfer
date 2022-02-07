using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Transfer.API.Configurations;

namespace Transfer.API
{
    public class Startup
    {
        public static string SwaggerDocTitle => "Transfer.Api";
        public static string SwaggerDocVersion => "v1";
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDataBase(Configuration);
            services.ConfigureDI();
            services.AddControllers();
            services.ConfigureSwagger<Startup>(SwaggerDocTitle, SwaggerDocVersion);
            var assembly = AppDomain.CurrentDomain.Load("Transfer.Application");
            services.AddMediatR(assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfig(SwaggerDocTitle, SwaggerDocVersion);
            }

            app.UseGlobalization(Configuration);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
