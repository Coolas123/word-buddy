using Application.Dictionaries.Commands.CreateDictionaryRow;
using Application.Dictionaries.Commands.UpdateDictionary;
using Application.Dictionaries.Commands.UpdateDictionaryAndRows;
using Application.Dictionaries.Queries.GetDictionaries;
using Application.Dictionaries.Queries.GetDictionary;
using Application.Translations.Commands.UpdateTranslation;
using Application.Words.Commands.UpdateWord;
using Application.Words.Commands.UpdateWord.UpdateWords;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("Dictionaries")]
    [Produces("application/json")]
    public class DictionaryController : ControllerBase
    {

        private readonly ISender sender;
        public DictionaryController(ISender sender) {
            this.sender = sender;
        }

        /// <summary>
        /// Возврат всех словарей пользователя
        /// </summary>
        /// <returns>Список всех словарей пользователя</returns>
        /// <response code="200">Возврат списка словарей</response>
        /// <response code="400">Словари не найдены</response>
        [Authorize(Roles="User")]
        [HttpGet]
        public async Task<IActionResult> Get() {
            var dictionariesResult = await sender.Send(new GetDictionariesQuery
            {
                UserId = Guid.Parse(User.FindFirstValue("Id"))
            });

            if (dictionariesResult.IsSuccess) {

                return StatusCode(200,dictionariesResult.Value());
            }

            return StatusCode(400,"Не удалось найти словари");
        }

        /// <summary>
        /// Создание словаря
        /// </summary>
        /// <param name="model">Модель словаря и строк со словами</param>
        /// <returns>Сообщение успешности операции или список ошибок</returns>
        /// <response code="200">Словарь успешно создан</response>
        /// <response code="400">Список ошибок неудачи создания словаря</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateDictionaryAndRowsCommand model) {
            model.CreateDictionaryCommand.CreatedAt = DateTime.Now.ToUniversalTime();

            var CreateDictionaryResult = await sender.Send(model);
            
            if (!CreateDictionaryResult.IsSuccess) {
                var validationError = (IValidationResult)CreateDictionaryResult;

                return StatusCode(400,validationError);
            }
            else {
                return StatusCode(200,"Словарь создан!");
            }
        }

        /// <summary>
        /// Просмотр словаря с его содержимым
        /// </summary>
        /// <param name="dictionaryId">id словаря</param>
        /// <returns>Словарь с его содержимым</returns>
        /// <response code="200">Возврат словаря</response>
        /// <response code="400">Словарь не найден</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("GetDictionariesAndRows/{dictionaryId:Guid}")]
        public async Task<IActionResult> GetDictionariesAndRows(Guid dictionaryId) {
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

                return StatusCode(200, new UpdateDictionaryAndRowsCommand
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

            return StatusCode(400,"Не удалось найти словарь");
        }

        /// <summary>
        /// Обновление словаря
        /// </summary>
        /// <param name="model">Модель со словарем и его содержимым</param>
        /// <returns>Код состояния</returns>
        /// <response code="200">Словарь обновлен</response>
        /// <response code="400">Не удалось обновить словарь</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPut]
        public async Task<IActionResult> Put(UpdateDictionaryAndRowsCommand model) {
            model.UpdateDictionaryCommand.LastViewedAt = DateTime.Now.ToUniversalTime();

            model.UpdateDictionaryCommand.UserId = Guid.Parse(User.FindFirstValue("Id"));

            var dictionaryResult = await sender.Send(model);

            if (dictionaryResult.IsSuccess) {
                return StatusCode(200,"Словарь обновлен!");
            }

            return StatusCode(400,"Не удалось обновить словарь");
        }
    }
}
