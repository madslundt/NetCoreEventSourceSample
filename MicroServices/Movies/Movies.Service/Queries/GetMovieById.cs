using EventFlow.EntityFramework;
using EventFlow.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movies.ReadStore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Service.Queries
{
    public class GetMovieById
    {
        public class Query : IQuery<Result>
        {
            public Guid Id { get; }
            public Query(Guid id)
            {
                Id = id;
            }
        }

        public class Result
        {
            public string Title { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(query => query.Id).NotEmpty();
            }
        }

        internal class Handler : IQueryHandler<Query, Result>
        {
            private readonly IDbContextProvider<MovieContext> _movieContext;

            public Handler(IDbContextProvider<MovieContext> movieContext)
            {
                _movieContext = movieContext;
            }

            public async Task<Result> ExecuteQueryAsync(Query query, CancellationToken cancellationToken)
            {
                using (var context = _movieContext.CreateContext())
                {
                    var movie = await GetMovie(query.Id, context);

                    return movie;
                }
            }

            private async Task<Result> GetMovie(Guid id, MovieContext context)
            {
                var query = from movie in context.Movies
                            where movie.Id == id
                            select new Result
                            {
                                Title = movie.Title
                            };

                var result = await query.FirstOrDefaultAsync();

                return result;
            }
        }
    }
}
