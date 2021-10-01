using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bets.Commands.CreateBet;
using Application.Bets.Commands.DeleteBet;
using Application.Bets.Commands.UpdateBet;
using Application.Bets.Queries;
using Application.Bets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class BetController : ApiController
    {
        /// <summary>
        /// Creates a bet and stores it to the database
        /// </summary>
        /// <param name="command">Request's payload</param>
        /// <returns>The created bet</returns>
        /// <example>
        ///     POST /api/Bet
        ///     "betType": 1
        ///     "amount": 24
        /// </example>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<BetVm>> Create(CreateBetCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Updates a bet
        /// </summary>
        /// <param name="command">Request's payload</param>
        /// <returns>The updated bet</returns>
        /// <example>
        ///     PUT /api/Bet
        ///     "betType": 1
        ///     "amount": 24
        /// </example>
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<BetVm>> Update(UpdateBetCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Deletes a bet
        /// </summary>
        /// <param name="id">The id of bet</param>
        /// <returns>NoContent</returns>
        /// <example>
        ///     DELETE /api/Bet/1
        /// </example>
        [HttpDelete("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBetCommand {Id = id});
            
            return NoContent();
        }

        /// <summary>
        /// Displays the list of all created bets
        /// </summary>
        /// <returns>The list of all bets</returns>
        /// <example>
        ///     GET /api/Bet
        /// </example>
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<List<BetVm>>> Get()
        {
            var bets = await Mediator.Send(new GetBetsQuery());
            return bets.ToList();
        }

        /// <summary>
        /// Displays single bet with the id provided
        /// </summary>
        /// <param name="id">The id of bet</param>
        /// <returns>Single bet</returns>
        /// <example>
        ///     GET /api/Bet/1
        /// </example>
        [HttpGet("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<BetVm>> GetById(int id)
        {
            var bet = await Mediator.Send(new GetBetByIdQuery {Id = id});
            return bet;
        }
    }
}