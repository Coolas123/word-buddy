using Application.Dictionaries.Commands.CreateDictionaryRow;
using Application.Dictionaries.Commands.UpdateDictionary;
using Application.Dictionaries.Commands.UpdateDictionaryAndRows;
using Application.Dictionaries.Commands.UpdateDictionaryRow;
using Application.Dictionaries.Queries.GetDictionary;
using Application.Dictionaries.Queries.GetPagingDictionaries;
using Application.Translations.Commands.UpdateTranslation;
using Application.Words.Commands.UpdateWord;
using Application.Words.Commands.UpdateWord.UpdateWords;
using Domain.Shared;
using Domain.TagHelperModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Extensions;
using Presentation.ViewModels;
using System.Reflection;
using System.Security.Claims;

namespace Presentation.Controllers
{

    public class DictionaryController : Controller
    {
        private int PageSize = 8;

        private readonly ISender sender;
        public DictionaryController(ISender sender) {
            this.sender = sender;
        }
        [Authorize(Roles="User")]
        [HttpGet]
        public async Task<IActionResult> GetDictionaries(int currentPage=1) {
            var dictionariesResult = await sender.Send(new GetPagingDictionariesQuery
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize
                },
                UserId = Guid.Parse(User.FindFirstValue("Id"))
            });

            if (dictionariesResult.IsSuccess) {
                var (pagingInfo, dictionaries) = dictionariesResult.Value();

                return View(new DictionariesViewModel
                {
                    Dictionaries = dictionaries,
                    PagingInfo = pagingInfo
                });
            }

            return View(new DictionariesViewModel());
        }

        [Authorize(Roles = "WordLearner")]
        [HttpGet]
        public IActionResult CreateDictionary() {
            return View(new CreateDictionaryAndRowsCommand());
        }

        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> CreateDictionary(CreateDictionaryAndRowsCommand model) {
            model.CreateDictionaryCommand.CreatedAt = DateTime.Now.ToUniversalTime();

            var CreateDictionaryResult = await sender.Send(model);
            
            if (!CreateDictionaryResult.IsSuccess) {
                var validationError = (IValidationResult)CreateDictionaryResult;
                ModelState.AddModelError(validationError.Errors);
                return View(model);
            }
            else {
                TempData["DictionaryCreated"] = "Словарь создан";
            }

            return Redirect("GetDictionaries");
        }

        [Authorize(Roles = "WordLearner")]
        [HttpGet("{dictionaryId:Guid}")]
        public async Task<IActionResult> GetDictionaries(Guid dictionaryId) {
            var dictionaryResult = await sender.Send(new GetDictionaryQuery { DictionaryId= dictionaryId });

            if (dictionaryResult.IsSuccess) {
                var viewWords = new List<UpdateWordCommand>(dictionaryResult.Value().Words.Count);

                foreach (var word in dictionaryResult.Value().Words) {
                    viewWords.Add(new UpdateWordCommand
                    {
                        Id = word.Id,
                        Text = word.Text,
                        LearnStatus = word.LearnStatus,
                        LearnStatusChangedAt = word.LearnStatusChangedAt,
                        Translation = new UpdateTranslationCommand
                        {
                            WordId = word.Translation.WordId,
                            Text = word.Translation.Text,
                        }
                    });
                }

                return View("GetDictionary", new UpdateDictionaryAndRowsCommand
                {
                    UpdateDictionaryCommand = new UpdateDictionaryCommand
                    {
                        Title = dictionaryResult.Value().Title,
                        Description = dictionaryResult.Value().Description,
                        LastViewedAt = dictionaryResult.Value().LastViewedAt,
                        WordLanguage = dictionaryResult.Value().WordLanguage,
                        TranslationLanguage = dictionaryResult.Value().TranslationLanguage,
                        DictionaryId = dictionaryResult.Value().Id
                    },
                    UpdateWordsCommand = new UpdateWordsCommand
                    {
                        Words = viewWords,
                        DictionaryId = dictionaryResult.Value().Id
                    }
                });
            }

            return Redirect("GetDictionary");
        }

        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> UpdateDictionary(UpdateDictionaryAndRowsCommand model) {
            model.UpdateDictionaryCommand.LastViewedAt = DateTime.Now.ToUniversalTime();
            model.UpdateDictionaryCommand.UserId = Guid.Parse(User.FindFirstValue("Id"));
            var dictionaryResult = await sender.Send(model);


            return Redirect("GetDictionaries");
        }
    }
}
