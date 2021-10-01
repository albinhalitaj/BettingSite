#nullable enable
using System.Threading.Tasks;
using Application.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class IdentityController : ApiController
    {
        /// <summary>
        ///  Registers a new user and stores it to the database 
        /// </summary>
        /// <param name="command">Request's payload</param>
        /// <returns>NoContent</returns>
        /// <example>
        ///     POST /api/identity/register
        ///     "username": "test"
        ///     "email": "test@test.com"
        ///     "password": "test1234"
        /// </example>
        [Route(nameof(Register))]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<string>> Register(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Validates the user credentials and logs in the user
        /// </summary>
        /// <param name="command">Request's payload</param>
        /// <returns>User id and the token</returns>
        ///  <example>
        ///     POST /api/identity/login
        ///     "username": "test"
        ///     "password": "test1234"
        /// </example>
        [Route(nameof(Login))]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<object?>> Login(LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result != null && string.IsNullOrEmpty(result.ToString()))
            {
                return Unauthorized();
            }

            return result;
        } 
    }
}