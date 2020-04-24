using Microsoft.EntityFrameworkCore;
using Movies.ReadStore.ReadModels;

namespace Movies.ReadStore
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<MovieReadModel> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MovieReadModel>(b =>
            {
                b.Property(p => p.Id)
                    .IsRequired();

                b.Property(p => p.CreatedUtc)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                
                b.HasIndex(i => i.Id).IsUnique();
            });
        }
    }
}
