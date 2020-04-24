using EventFlow.EntityFramework;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Reviews.ReadStore
{
    public class ReviewContextProvider : IDbContextProvider<ReviewContext>
    {
        private readonly DbContextOptions<ReviewContext> _options;

        public ReviewContextProvider(EnvironmentConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ReviewContext>()
                .UseSqlServer(configuration.DbConnection)
                .Options;
        }

        public ReviewContext CreateContext()
        {
            var db = new ReviewContext(_options);
            db.Database.EnsureCreated();

            return db;
        }
    }
}
