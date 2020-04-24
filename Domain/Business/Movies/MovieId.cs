using EventFlow.Core;

namespace Domain.Business.Movies
{
    public class MovieId : Identity<MovieId>
    {
        public MovieId(string value) : base(value)
        {
        }
    }
}