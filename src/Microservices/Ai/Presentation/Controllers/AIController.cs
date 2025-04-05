using Application.TextGenerator.Queries.GenerateText;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AIController : ControllerBase
    {
        private readonly ISender sender;

        public AIController(ISender sender) {
            this.sender = sender;
        }


        [HttpPost("GenerateWordContext")]
        [AllowAnonymous]
        public async Task<IActionResult> PostGenerateWordContext([FromBody]GenerateTextQuery model) {
            var res = await sender.Send(model);
            return StatusCode(200);
         }
    }
}
