using EventFlow.Entities;
using System;

namespace Domain.Business.Reviews
{
    public class ReviewEntity : Entity<ReviewId>
    {
        public ReviewEntity(ReviewId id) : base(id)
        {}

        public string MovieId { get; set; }
        public ReviewRatingEnum Rating { get; set; }
        public ReviewStatusEnum Status { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
