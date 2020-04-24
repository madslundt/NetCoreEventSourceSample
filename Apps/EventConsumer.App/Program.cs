using Domain.Business.Movies;
using Domain.Business.Movies.Events;
using Domain.Module;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.RabbitMQ;
using EventFlow.RabbitMQ.Extensions;
using EventFlow.Subscribers;
using Infrastructure.Configurations;
using Infrastructure.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EventConsumer.App
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", true, true);
                    config.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", true, true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices(
                    (hostcontext, services) =>
                    {
                        var envconfig = EnvironmentConfiguration.Bind(hostcontext.Configuration);
                        services.AddSingleton(envconfig);

                        EventFlowOptions.New
                            .Configure(cfg => cfg.IsAsynchronousSubscribersEnabled = true)
                            .UseServiceCollection(services)
                            .AddAspNetCore()
                            .PublishToRabbitMq(RabbitMqConfiguration.With(new Uri($"{envconfig.RabbitMqConnection}"),
                                true, 5, envconfig.RabbitExchange))
                            .RegisterModule<DomainModule>()

                            .AddAsynchronousSubscriber<MovieAggregate, MovieId, MovieCreatedEvent, RabbitMqConsumePersistanceService>()
                            .RegisterServices(s =>
                            {
                                s.Register<IHostedService, RabbitConsumePersistenceService>(Lifetime.Singleton);
                                s.Register<IHostedService, RabbitMqConsumePersistanceService>(Lifetime.Singleton);
                            });
                    })
                .ConfigureLogging((hostingContext, logging) => { });

            await builder.RunConsoleAsync();
        }

    }

    public interface IRabbitMqConsumerPersistanceService
    {

    }

    public class RabbitMqConsumePersistanceService : IHostedService, IRabbitMqConsumerPersistanceService, ISubscribeAsynchronousTo<MovieAggregate, MovieId, MovieCreatedEvent>
    {

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<MovieAggregate, MovieId, MovieCreatedEvent> @event, CancellationToken cancellationToken)
        {

            Console.WriteLine($"Movie created {@event.AggregateEvent.Movie.Title}");

            return Task.CompletedTask;
        }

    }
}
