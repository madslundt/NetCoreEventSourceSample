using Domain.Business.Movies;
using Domain.Business.Movies.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Infrastructure.ReadStores;
using System;

namespace Movies.ReadStore.ReadModels
{
    public class MovieReadModel : VersionedReadModel,
        IAmReadModelFor<MovieAggregate, MovieId, MovieCreatedEvent>
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public int Year { get; private set; }

        public DateTimeOffset CreatedUtc { get; private set; } = DateTimeOffset.UtcNow;

        public void Apply(IReadModelContext context, IDomainEvent<MovieAggregate, MovieId, MovieCreatedEvent> @event)
        {
            var _movie = @event.AggregateEvent.Movie;

            Id = MovieId.NewComb().GetGuid();
            AggregateId = @event.AggregateIdentity.Value;
            Title = _movie.Title;
            Year = _movie.Year;
        }

        public MovieEntity ToMovie()
        {
            return new MovieEntity(MovieId.With(AggregateId))
            {
                CreatedUtc = CreatedUtc,
                Title = Title,
                Year = Year
            };
        }
    }
}
