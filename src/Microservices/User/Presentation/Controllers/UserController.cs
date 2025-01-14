using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.LoginUser;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly ISender sender;

        public UserController(ISender sender) {
            this.sender = sender;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model">Модель логина</param>
        /// <returns>возвращает массив ValidationResult</returns>
        /// <response code="200">Пользователь авторизован</response>
        /// <response code="400">Неверно введенные данные</response>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLogin(LoginUserQuery model) {
            var token = await sender.Send(model);
            if (token.IsSuccess) {
                HttpContext.Response.Cookies.Append("token", token.Value());
                return StatusCode(200, token.Value());
            }
            else {
                var validationResult = (IValidationResult)token;

                return StatusCode(400, validationResult.Errors);
            }
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Моедль регистрации</param>
        /// <returns>Сообщение успешности создания</returns>
        /// <response code="201">Пользователь создан</response>
        /// <response code="400">Неверные данные для создания пользователя</response>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> PostRegistration(RegisterUserCommand model) {
            var token = await sender.Send(model);
            if (token.IsSuccess) {
                HttpContext.Response.Cookies.Append("token", token.Value());
                return StatusCode(201,"Регистрация прошла успешно!");
            }
            else {
                var validationResult = (IValidationResult)token;

                return StatusCode(400, validationResult.Errors);
            }
        }

        /// <summary>
        /// Удаление JWT токена из куки
        /// </summary>
        /// <returns>Код состояния</returns>
        /// <response code="200">Токен успешно удален</response>
        [HttpGet("LogOut")]
        [Authorize]
        public IActionResult LogOut() {
            HttpContext.Response.Cookies.Delete("token");
            return StatusCode(200);
        }
    }
}
