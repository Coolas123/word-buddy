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
        /// User authorization
        /// </summary>
        /// <param name="model">Email and password model</param>
        /// <returns>Returns a success message or a list of errors</returns>
        /// <response code="200">The authorization was successful. Returns a success flag, jwt token and a success of operation message/response>
        /// <response code="400">The request was a failure. Returns a success flag and a list of errors</response>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLogin([FromBody]LoginUserQuery model) {
            var token = await sender.Send(model);
            if (token.IsSuccess) {
                HttpContext.Response.Cookies.Append("token", token.Value());
                return StatusCode(200, new {
                    success = true,
                    jwt = token.Value(),
                    message = "Авторизация прошла успешно!"
                });
            }
            else {
                var validationResult = (IValidationResult)token;

                return StatusCode(400, new
                {
                    success = false,
                    errors = validationResult.Errors
                });
            }
        }

        /// <summary>
        /// Registration a new user
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <returns>Returns a success message or a list of errors</returns>
        /// <response code="201">The registration was successful. Returns a success flag, jwt token and a success of operation message</response>
        /// <response code="400">The request was a failure. Returns a list of errors</response>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> PostRegistration([FromBody]RegisterUserCommand model) {
            var token = await sender.Send(model);
            if (token.IsSuccess) {
                HttpContext.Response.Cookies.Append("token", token.Value());
                return StatusCode(201, new
                {
                    success = true,
                    jwt = token.Value(),
                    message = "Регистрация прошла успешно!"
                });
            }
            else {
                var validationResult = (IValidationResult)token;

                return StatusCode(400, validationResult.Errors);
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>Returns a http status code</returns>
        /// <response code="200">The jwt token was deleted from cookie</response>
        [HttpGet("LogOut")]
        [Authorize]
        public IActionResult LogOut() {
            HttpContext.Response.Cookies.Delete("token");
            return StatusCode(200);
        }
    }
}
