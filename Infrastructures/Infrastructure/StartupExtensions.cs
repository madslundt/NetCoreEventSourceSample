using FluentValidation.AspNetCore;
using Infrastructure.Configurations;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, EnvironmentConfiguration env, Type type, OpenApiInfo apiInfo)
        {
            services
                .AddSingleton(env)
                .AddSwaggerGen(c => c.SwaggerDoc(apiInfo.Version, apiInfo))
                .AddMvc(opt => { opt.Filters.Add(typeof(ExceptionFilter)); })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining(type); });

            services
                .AddControllers()
                .AddNewtonsoftJson();

            return services;
        }

        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env, Action<SwaggerUIOptions> swaggerUIOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
