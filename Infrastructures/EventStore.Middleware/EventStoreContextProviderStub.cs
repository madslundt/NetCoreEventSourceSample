using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Middleware
{
    public class EventStoreContextProviderStub : IDbContextProvider<EventSourcingDbContext>
    {
        public EventSourcingDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<EventSourcingDbContext>()
                .UseInMemoryDatabase("For testing")
                .Options;

            var context = new EventSourcingDbContext(options);
            return context;
        }
    }
}
