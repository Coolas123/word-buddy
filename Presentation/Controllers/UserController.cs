using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.LoginUser;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Extensions;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly ISender sender;

        public UserController(ISender sender) {
            this.sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new LoginUserQuery { ReturnUrl = "/" });
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserQuery model, string returnUrl) {
            var token = await sender.Send(model);
            if (token.IsSuccess) {
                HttpContext.Response.Cookies.Append("token", token.Value());
                return Redirect(returnUrl ?? "/");
            }
            else {
                var validationResult = (IValidationResult)token;

                ModelState.AddModelError(validationResult.Errors);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new RegisterUserCommand { ReturnUrl = "/" });
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegisterUserCommand model, string returnUrl) {
            var token = await sender.Send(model);
            if (token.IsSuccess) {
                HttpContext.Response.Cookies.Append("token", token.Value());
                return Redirect(returnUrl ?? "/");
            }
            else {
                var validationResult = (IValidationResult)token;

                ModelState.AddModelError(validationResult.Errors);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult LogOut() {
            HttpContext.Response.Cookies.Delete("token");
            return RedirectToAction("Index", "Home");
        }
    }
}
