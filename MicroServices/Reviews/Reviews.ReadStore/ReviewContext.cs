using Microsoft.EntityFrameworkCore;
using Reviews.ReadStore.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviews.ReadStore
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions<ReviewContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<ReviewReadModel> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReviewReadModel>(b =>
            {
                b.Property(p => p.Id)
                    .IsRequired();

                b.Property(p => p.CreatedUtc)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.Status)
                    .IsRequired();

                b.Property(p => p.Rating)
                    .IsRequired();

                b.HasKey(k => k.Id);
            });
        }
    }
}
