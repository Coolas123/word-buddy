using Application.Users.Commands.ChangeSettings;
using Application.Users.Queries.GetUser;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.ViewModels;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Produces("application/json")]
    public class SettingController : ControllerBase
    {
        private readonly ISender sender;

        public SettingController(ISender sender) {
            this.sender = sender;
        }

        /// <summary>
        /// Get user settings
        /// </summary>
        /// <returns>UserName, Email, Country model</returns>
        /// <response code="200">The request was successful. Returns the model</response>
        /// <response code="400">The request was a failure. The user was not found</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() {
            var UserId = Guid.Parse(User.FindFirstValue("Id"));

            var patronResult = await sender.Send(new GetUserQuery(UserId));

            if (patronResult.IsSuccess) {
                return StatusCode(200, SettingUserViewModel.Create(patronResult.Value()));
            }
            return StatusCode(400,"Пользователь не найден");
        }

        /// <summary>
        /// Update user settings
        /// </summary>
        /// <param name="model">New user settings model</param>
        /// <returns>Returns a success message or a list of errors</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns a list of errors</response>
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Patch([FromBody]ChangeUserSettingsCommand model) {
            model.UserId = Guid.Parse(User.FindFirstValue("Id"));

            var result = await sender.Send(model);

            if (result.IsSuccess && result.Value()) {
                await HttpContext.SignOutAsync();

                return StatusCode(200,"Необходимо перезайти в аккаунт");
            }
            else if (result.IsSuccess) {
                return StatusCode(200, "Настройки успешно обновлены");
            }
            else {
                if (result is IValidationResult) {
                    var r = (IValidationResult)result;
                    return StatusCode(400, r.Errors);
                }
                else {
                    return StatusCode(400, result.Error);
                }
            }
        }
    }
}
