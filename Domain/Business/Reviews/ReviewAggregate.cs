using EventFlow.Aggregates;

namespace Domain.Business.Reviews
{
    public class ReviewAggregate : AggregateRoot<ReviewAggregate, ReviewId>
    {
        private readonly ReviewAggregateState _reviewState = new ReviewAggregateState();

        public ReviewAggregate(ReviewId id) : base(id)
        {
            Register(_reviewState);
        }
    }
}
