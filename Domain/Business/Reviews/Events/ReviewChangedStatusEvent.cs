using EventFlow.Aggregates;

namespace Domain.Business.Reviews.Events
{
    public class ReviewChangedStatusEvent : AggregateEvent<ReviewAggregate, ReviewId>
    {
        public ReviewStatusEnum Status { get; }

        public ReviewChangedStatusEvent(ReviewStatusEnum status)
        {
            Status = status;
        }
    }
}
