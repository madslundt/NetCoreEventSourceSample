using System;
using Domain.Module;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using EventFlow.Extensions;
using EventFlow.RabbitMQ;
using EventFlow.RabbitMQ.Extensions;
using EventStore.Middleware.Module;
using Infrastructure;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Movies.ReadStore;
using Movies.ReadStore.Module;

namespace Movies.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var env = EnvironmentConfiguration.Bind(_configuration);

            services.ConfigureServices(env, new OpenApiInfo
            {
                Title = "Movies API",
                Version = "v1"
            });

            EventFlowOptions.New
                .UseServiceCollection(services)
                .AddDefaults(typeof(Startup).Assembly)
                .AddAspNetCore()
                .UseConsoleLog()
                .RegisterModule<DomainModule>()
                .RegisterModule<MovieReadStoreModule>()
                .RegisterModule<EventSourcingModule>()
                .PublishToRabbitMq(RabbitMqConfiguration.With(new Uri(env.RabbitMqConnection)))
                .CreateServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<IDbContextProvider<MovieContext>>();
                dbContext.CreateContext();
            }

            app.Configure(env, c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API V1"); });
        }
    }
}
