using Application.Dictionaries.Commands.CreateDictionaryRow;
using Application.Dictionaries.Commands.UpdateDictionaryAndRows;
using Application.Dictionaries.Queries.GetDictionaries;
using Application.Dictionaries.Queries.GetDictionary;
using Application.DictionaryRow.Commands.UpdateWord;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
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
        /// Get all users dictionaries
        /// </summary>
        /// <returns>Returns a list of all users dictionaries or an error message</returns>
        /// <response code="200">The request was successful. Returns a list of all users dictionaries</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles="User")]
        [HttpGet]
        public async Task<IActionResult> Get() {
            var dictionariesResult = await sender.Send(new GetDictionariesQuery
            {
                UserId = Guid.Parse(User.FindFirstValue("Id"))
            });

            if (dictionariesResult.IsSuccess) {
                return StatusCode(200, DictionariesViewModel.CreateArray(dictionariesResult.Value()));
            }

            return StatusCode(400,"Не удалось найти словари");
        }

        /// <summary>
        /// Create a new dictionary
        /// </summary>
        /// <param name="model">New dictionary model</param>
        /// <returns>Returns a success message or a list of errors</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateDictionaryAndRowsCommand model) {
            model.CreateDictionaryCommand.LastViewedAt = DateTime.Now.ToUniversalTime();
            model.CreateDictionaryCommand.UserId = Guid.Parse(User.FindFirstValue("Id"));

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
        /// Get dictionary
        /// </summary>
        /// <param name="id">Dictionary id</param>
        /// <returns>Returns a ditionary</returns>
        /// <response code="200">The request was successful. Returns a dictionary</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDictionariesAndRows(string id) {
            var dictionaryResult = await sender.Send(new GetDictionaryQuery { DictionaryId= Guid.Parse(id) });

            if (dictionaryResult.IsSuccess) {
                var viewRows = new List<UpdateDictionaryRowCommand>(dictionaryResult.Value().DictionaryRows.Count);

                foreach (var word in dictionaryResult.Value().DictionaryRows) {
                    viewRows.Add(new UpdateDictionaryRowCommand
                    {
                        Id = word.Id,
                        WordText = word.WordText,
                        LearnStatus = word.LearnStatus,
                        LearnStatusChangedAt = word.LearnStatusChangedAt,
                        WordTranslation = word.WordTranslation
                    });
                }

                return StatusCode(200, UpdateDictionaryAndRowsCommand.Create(dictionaryResult.Value(),viewRows));
            }

            return StatusCode(400,"Не удалось найти словарь");
        }

        /// <summary>
        /// Update dictionary
        /// </summary>
        /// <param name="model">Update dictionary model</param>
        /// <returns>Returns a success or error message</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
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
