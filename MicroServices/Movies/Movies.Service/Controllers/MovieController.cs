using Domain.Business.Movies.Commands;
using EventFlow;
using EventFlow.Queries;
using FluentValidation;
using Infrastructure.Controller;
using Microsoft.AspNetCore.Mvc;
using Movies.Service.Queries;
using Movies.Service.VIewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Service.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MovieController : OurController
    {
        public MovieController(ICommandBus commandBus, IQueryProcessor queryProcessor, IValidatorFactory validationFactory) : base (commandBus, queryProcessor, validationFactory)
        {}

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await ProcessAsync(new GetMovieById.Query(id), cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieViewModel model, CancellationToken cancellationToken)
        {
            var result = await PublishAsync(new CreateMovieCommand.Command(model.Title, model.Year), cancellationToken);
            var id = result.AggregateRoot.GetIdentity();

            return Ok(new
            {
                Id = id.Value
            });
        }
    }
}