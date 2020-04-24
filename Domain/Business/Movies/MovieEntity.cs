using Domain.Business.Reviews;
using EventFlow.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Business.Movies
{
    public class MovieEntity : Entity<MovieId>
    {
        public MovieEntity(MovieId id) : base(id)
        { }

        public string Title { get; set; }
        public int Year { get; set; }
        public HashSet<ReviewEntity> Reviews { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
