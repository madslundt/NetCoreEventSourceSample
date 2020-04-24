using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using FluentValidation;
using Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Business.Movies.Commands
{
    public class CreateMovie
    {
        public class Command : Command<MovieAggregate, MovieId, CommandReturnResult>
        {
            public string Title { get; }
            public int Year { get; }

            public Command(string title, int year) : base(MovieId.New)
            {
                Title = title;
                Year = year;
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(command => command.Title).NotEmpty();
                RuleFor(command => command.Year).GreaterThan(1900);
            }
        }

        internal class Handler : CommandHandler<MovieAggregate, MovieId, CommandReturnResult, Command>
        {
            public override async Task<CommandReturnResult> ExecuteCommandAsync(MovieAggregate aggregate, Command command, CancellationToken cancellationToken)
            {
                var agg = aggregate.CreateMovie(command.Title, command.Year);

                return await Task.FromResult(new CommandReturnResult(agg.IsSuccess, aggregate));
            }
        }
    }
}
