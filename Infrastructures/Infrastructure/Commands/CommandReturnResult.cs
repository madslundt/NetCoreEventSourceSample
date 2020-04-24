using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;

namespace Infrastructure.Commands
{
    public class CommandReturnResult : ExecutionResult
    {
        public CommandReturnResult(bool isSuccess, IAggregateRoot aggregateRoot)
        {
            IsSuccess = isSuccess;
            AggregateRoot = aggregateRoot;
        }

        public override bool IsSuccess { get; }
        public IAggregateRoot AggregateRoot { get; }
    }
}
