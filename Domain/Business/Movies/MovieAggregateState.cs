using Domain.Business.Movies.Events;
using EventFlow.Aggregates;
using System;

namespace Domain.Business.Movies
{
    [Serializable]
    public class MovieAggregateState : AggregateState<MovieAggregate, MovieId, MovieAggregateState>,
        IApply<MovieCreatedEvent>
    {
        public MovieEntity Entity { get; set; }
        public void Apply(MovieCreatedEvent @event)
        {
            Entity = @event.Movie;
        }
    }
}
