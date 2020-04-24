using Domain.Business.Reviews.Events;
using EventFlow.Aggregates;
using System;

namespace Domain.Business.Reviews
{
    [Serializable]
    public class ReviewAggregateState : AggregateState<ReviewAggregate, ReviewId, ReviewAggregateState>,
        IApply<ReviewCreatedEvent>
    {
        public ReviewEntity Entity { get; set; }

        public void Apply(ReviewCreatedEvent @event)
        {
            Entity = @event.Review;
        }

        public void Apply(ReviewChangedStatusEvent @event)
        {
            if (Entity.Status == ReviewStatusEnum.Deleted)
            {
                throw new Exception(""); // TODO
            }
            else if (Entity.Status == ReviewStatusEnum.Approved && @event.Status == ReviewStatusEnum.WaitingApproval)
            {
                throw new Exception(""); // TODO
            }

            Entity.Status = @event.Status;
        }
    }
}
