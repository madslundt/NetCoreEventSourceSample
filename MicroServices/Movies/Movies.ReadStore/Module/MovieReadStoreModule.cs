using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using Infrastructure.ReadStores;
using Movies.ReadStore.ReadModels;

namespace Movies.ReadStore.Module
{
    public class MovieReadStoreModule : IModule
    {
        public void Register(IEventFlowOptions options)
        {
            options.ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDefaults(typeof(MovieReadStoreModule).Assembly)
                .AddDbContextProvider<MovieContext, MovieContextProvider>()
                .UseEntityFrameworkEventStore<MovieContext>()
                .UseEntityFrameworkReadModel<MovieReadModel, MovieContext>()

                .RegisterServices(s =>
                {
                    s.Register<ISearchableReadModelStore<MovieReadModel>, EfSearchableReadStore<MovieReadModel, MovieContext>>();
                });
        }
    }
}
