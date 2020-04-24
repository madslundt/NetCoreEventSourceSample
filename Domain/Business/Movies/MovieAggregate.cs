using Domain.Business.Movies.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;

namespace Domain.Business.Movies
{
    public class MovieAggregate : AggregateRoot<MovieAggregate, MovieId>
    {
        private readonly MovieAggregateState _movieAggregateState = new MovieAggregateState();

        public MovieAggregate(MovieId id) : base(id)
        {
            Register(_movieAggregateState);
        }

        public IExecutionResult CreateMovie(string title, int year)
        {
            Emit(new MovieCreatedEvent(new MovieEntity(Id)
            {
                Title = title,
                CreatedUtc = DateTimeOffset.UtcNow,
                Year = year
            }));

            return ExecutionResult.Success();
        }
    }
}
