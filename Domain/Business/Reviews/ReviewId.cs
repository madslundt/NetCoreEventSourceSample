using EventFlow.Core;

namespace Domain.Business.Reviews
{
    public class ReviewId : Identity<ReviewId>
    {
        public ReviewId(string value) : base(value)
        {
        }
    }
}