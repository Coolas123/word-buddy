using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Extensions;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly ISender Sender;
        public DictionaryController(ISender sender) {
            Sender = sender;
        }
        [Authorize(Roles="User")]
        public IActionResult Index() {
            return View();
        }

        [Authorize(Roles = "WordLearner")]
        [HttpGet]
        public IActionResult CreateDictionary() {
            return View(new CreateDictionaryViewModel());
        }

        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> CreateDictionary(CreateDictionaryViewModel model) {
            model.CreateDictionaryCommand.CreatedAt = DateTime.Now.ToUniversalTime();
            
            var CreateDictionaryResult = await Sender.Send(model.CreateDictionaryCommand);
            
            if (!CreateDictionaryResult.IsSuccess) {
                var validationError = (IValidationResult)CreateDictionaryResult;
                ModelState.AddModelError(validationError.Errors);
                return View(model);
            }
            else {
                TempData["DictionaryCreated"] = "Словарь создан";
            }

            model.CreateDictionaryRowCommand.CreateWordCommand.DictionaryId = CreateDictionaryResult.Value();

            var createDictionaryRowResult = await Sender.Send(model.CreateDictionaryRowCommand);

            if (createDictionaryRowResult.IsFailure) {
                var validationError = (IValidationResult)createDictionaryRowResult;
                ModelState.AddModelError(validationError.Errors);
                return View(model);
            }

            return Redirect("Index");
        }
    }
}
