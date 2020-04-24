using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Middleware
{
    public class EventSourcingDbContext : DbContext
    {
        public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .AddEventFlowEvents()
                .AddEventFlowSnapshots();
        }
    }
}
