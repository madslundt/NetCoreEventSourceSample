using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using Infrastructure.ReadStores;
using Reviews.ReadStore.ReadModels;

namespace Reviews.ReadStore.Module
{
    public class ReviewReadStoreModule : IModule
    {
        public void Register(IEventFlowOptions options)
        {
            options.ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDefaults(typeof(ReviewReadStoreModule).Assembly)
                .AddDbContextProvider<ReviewContext, ReviewContextProvider>()
                .UseEntityFrameworkEventStore<ReviewContext>()
                .UseEntityFrameworkReadModel<ReviewReadModel, ReviewContext>()

                .RegisterServices(s =>
                {
                    s.Register<ISearchableReadModelStore<ReviewReadModel>, EfSearchableReadStore<ReviewReadModel, ReviewContext>>();
                });
        }
    }
}
