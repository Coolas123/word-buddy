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
        /// Взятие настроек пользователя
        /// </summary>
        /// <returns>Модель представления настроек пользователя SettingUserViewModel</returns>
        /// <response code="200">Настройки успешно найдены</response>
        /// <response code="204">Пользователь не найден</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() {
            var UserId = Guid.Parse(User.FindFirstValue("Id"));

            var patronResult = await sender.Send(new GetUserQuery(UserId));

            if (patronResult.IsSuccess) {
                return StatusCode(200, SettingUserViewModel.Create(patronResult.Value()));
            }
            return StatusCode(204,"Пользователь не найден");
        }

        /// <summary>
        /// Обновление настроек пользователя
        /// </summary>
        /// <param name="model">Модель настроек пользователя</param>
        /// <returns>Сообщение успешности операции или список ошибок</returns>
        /// <response code="200">Сообщение об успешно обновленных настройках</response>
        /// <response code="400">Список ошибок при неудаче обновить настройки</response>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ChangeUserSettingsCommand model) {
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
