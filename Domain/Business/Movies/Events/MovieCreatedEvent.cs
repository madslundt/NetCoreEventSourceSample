using EventFlow.Aggregates;

namespace Domain.Business.Movies.Events
{
    public class MovieCreatedEvent : AggregateEvent<MovieAggregate, MovieId>
    {
        public MovieEntity Movie { get; }

        public MovieCreatedEvent(MovieEntity movie)
        {
            Movie = movie;
        }
    }
}
