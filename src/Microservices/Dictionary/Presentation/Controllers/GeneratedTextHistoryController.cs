using Application.GeneratedTextHistory.Commands.SaveGeneratedTextHistory;
using Application.GeneratedTextHistory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("GeneratedTextHistory")]
    [Produces("application/json")]
    public class GeneratedTextHistoryController :ControllerBase
    {
        private readonly ISender sender;
        public GeneratedTextHistoryController(ISender sender) {
            this.sender = sender;
        }

        /// <summary>
        /// Save text context
        /// </summary>
        /// <param name="text">text context</param>
        /// <returns>Returns a result message</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveGeneratedTextHistoryCommand command) {
            command.UserId = Guid.Parse(User.FindFirstValue("Id"));
            var result = await sender.Send(command);

            if (result.IsSuccess) {
                return StatusCode(200, "Контекст сохранен");
            }

            return StatusCode(400, "Не сохранить контекст");
        }

        /// <summary>
        /// Get text contexts
        /// </summary>
        /// <returns>Returns a result message</returns>
        /// <response code="200">The request was successful. Returns a list of contexts</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet]
        public async Task<IActionResult> Get() {
            var result = await sender.Send(new GetGeneratedTextHistoryQuery { UserId = Guid.Parse(User.FindFirstValue("Id"))});

            if (result.IsSuccess) {
                return StatusCode(200, result.Value());
            }

            return StatusCode(400, "Контексты не найдены");
        }
    }
}
