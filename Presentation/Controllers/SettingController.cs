using Application.Users.Queries.GetUser;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Controllers.Extensions;
using Presentation.ViewModels;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SettingController : Controller
    {
        private readonly ISender sender;
        private readonly LinkGenerator linkGenerator;

        public SettingController(ISender sender, LinkGenerator linkGenerator) {
            this.sender = sender;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index() {
            var UserId = Guid.Parse(User.FindFirstValue("Id"));

            var patronResult = await sender.Send(new GetUserQuery(UserId));
            if (patronResult.IsSuccess) {
                return View(new SettingViewModel
                (
                    SettingUserViewModel.Create(patronResult.Value())
                ));
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateSettings(SettingViewModel model) {
            var result = await sender.Send(model.ChangeUserSettingsCommand!);

            if (result.IsSuccess && result.Value()) {
                await HttpContext.SignOutAsync();

                var path = linkGenerator.GetPathByAction("Index", "Setting")!;

                return RedirectToAction("Login", "User", new { returnUrl = path });
            }
            else if (result.IsFailure) {
                var validationResult = (IValidationResult)result;

                ModelState.AddModelError(validationResult.Errors);
            }

            var userResult = await sender.Send(new GetUserQuery(model.ChangeUserSettingsCommand.UserId));

            if (userResult.IsSuccess) {
                return View("Index", new SettingViewModel
                (
                    SettingUserViewModel.Create(userResult.Value())
                ));
            }

            return RedirectToAction("Index");
        }
    }
}
