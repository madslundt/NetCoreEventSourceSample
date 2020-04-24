using EventFlow.Aggregates;

namespace Domain.Business.Reviews.Events
{
    public class ReviewCreatedEvent : AggregateEvent<ReviewAggregate, ReviewId>
    {
        public ReviewEntity Review { get; }

        public ReviewCreatedEvent(ReviewEntity review)
        {
            Review = review;
        }
    }
}
