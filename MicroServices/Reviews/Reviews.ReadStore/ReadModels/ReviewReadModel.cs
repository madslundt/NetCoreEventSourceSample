using Domain.Business.Reviews;
using Domain.Business.Reviews.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Infrastructure.ReadStores;
using System;

namespace Reviews.ReadStore.ReadModels
{
    public class ReviewReadModel : VersionedReadModel,
        IAmReadModelFor<ReviewAggregate, ReviewId, ReviewCreatedEvent>
    {
        public Guid Id { get; set; }

        public ReviewRatingEnum Rating { get; set; }
        public ReviewStatusEnum Status { get; set; }

        public virtual DateTimeOffset CreatedUtc { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<ReviewAggregate, ReviewId, ReviewCreatedEvent> @event)
        {
            var _entity = @event.AggregateEvent.Review;

            Id = @event.AggregateIdentity.GetGuid();
            AggregateId = _entity.MovieId;
            Rating = _entity.Rating;
            Status = ReviewStatusEnum.WaitingApproval;
            CreatedUtc = DateTimeOffset.UtcNow;
        }
    }
}
