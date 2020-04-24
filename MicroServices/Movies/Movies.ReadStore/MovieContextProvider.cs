using EventFlow.EntityFramework;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Movies.ReadStore
{
    public class MovieContextProvider : IDbContextProvider<MovieContext>
    {
        private readonly DbContextOptions<MovieContext> _options;

        public MovieContextProvider(EnvironmentConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<MovieContext>()
                .UseSqlServer(configuration.DbConnection)
                .Options;
        }

        public MovieContext CreateContext()
        {
            var db = new MovieContext(_options);
            db.Database.EnsureCreated();

            return db;
        }
    }
}
