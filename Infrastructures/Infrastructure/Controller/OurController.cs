using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Queries;
using FluentValidation;
using Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Controller
{
    public class OurController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IValidatorFactory _validationFactory;

        public OurController(ICommandBus commandBus, IQueryProcessor queryProcessor, IValidatorFactory validationFactory)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _validationFactory = validationFactory;
        }

        protected async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            ValidateRequest(query);

            var result = await _queryProcessor.ProcessAsync(query, cancellationToken);

            if (result is null)
            {
                throw new ArgumentNullException();
            }

            return result;
        }

        protected async Task<TExecutionResult> PublishAsync<TAggregate, TIdentity, TExecutionResult>(Command<TAggregate, TIdentity, TExecutionResult> command, CancellationToken cancellationToken)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TExecutionResult : CommandReturnResult
        {
            ValidateRequest(command);

            var result = await _commandBus.PublishAsync(command, cancellationToken);

            if (result?.AggregateRoot is null)
            {
                throw new ArgumentNullException();
            }

            return result;
        }

        private void ValidateRequest<TRequest>(TRequest request)
        {
            var validator = _validationFactory.GetValidator(request.GetType());
            var result = validator?.Validate(new ValidationContext<TRequest>(request));

            if (result != null && !result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
