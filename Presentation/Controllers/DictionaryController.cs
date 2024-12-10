using Application.Dictionaries.Commands.CreateDictionary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> CreateDictionary(CreateDictionaryCommand command) {
            var CreateDictionaryResult = await Sender.Send(command);
            if (CreateDictionaryResult.IsSuccess) {
                TempData["DictionaryCreated"] = "Словарь создан";
            }
            else {
                TempData["DictionaryCreated"] = "Не удалось создать словарь";
            }
            return Redirect("Index");
        }
    }
}
