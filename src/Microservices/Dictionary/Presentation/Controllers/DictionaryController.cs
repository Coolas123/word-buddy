using Application.Dictionaries.Commands.CreateDictionaryRow;
using Application.Dictionaries.Commands.UpdateDictionaryAndRows;
using Application.Dictionaries.Queries.GetDictionaries;
using Application.Dictionaries.Queries.GetDictionary;
using Application.Dictionaries.Queries.GetDictionaryViewModelWithRows;
using Application.Dictionaries.Queries.GetFreeDicitonariesForCardPlan;
using Application.DictionaryRow.Commands.MoveToAnotherDicitonaryRows;
using Application.DictionaryRow.Commands.UpdateLearnStatus;
using Application.DictionaryRow.Commands.UpdateWord;
using Application.DictionaryRow.Queries.GetDictionaryWithRowsByLearnStatus;
using Domain.Enums;
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
            var r = Guid.Parse(User.FindFirstValue("Id"));
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
        public async Task<IActionResult> Post([FromBody]CreateDictionaryAndRowsCommand model) {
            model.CreateDictionaryCommand.LastViewedAt = DateTime.Now.ToUniversalTime();
            model.CreateDictionaryCommand.UserId = Guid.Parse(User.FindFirstValue("Id"));

            var CreateDictionaryResult = await sender.Send(model);
            
            if (CreateDictionaryResult.IsSuccess) {
                return StatusCode(200, "Словарь создан!");
            }
            else {
                var validationError = (IValidationResult)CreateDictionaryResult;

                return StatusCode(400, validationError);
            }
        }

        /// <summary>
        /// Get dictionary with rows
        /// </summary>
        /// <param name="id">Dictionary id</param>
        /// <returns>Returns a ditionary with rows</returns>
        /// <response code="200">The request was successful. Returns a dictionary with rows</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]string id) {
            var dictionaryResult = await sender.Send(new GetDictionaryQuery { DictionaryId= Guid.Parse(id) });

            if (dictionaryResult.IsSuccess) {
                var viewRows = new List<UpdateDictionaryRowCommand>(dictionaryResult.Value().DictionaryRows.Count);

                foreach (var row in dictionaryResult.Value().DictionaryRows) {
                    var imPathBase = @"../Application/img/";
                    byte[] imgByte = null;
                    if (!string.IsNullOrEmpty(row.ImgPath)) {
                        //using (MemoryStream ms = new MemoryStream()) {
                        //    var image = Image.FromFile(imPathBase + row.Id);
                        //    ImageConverter converter = new ImageConverter();
                        //    imgByte = (byte[])converter.ConvertTo(image, typeof(byte[]));
                        //}
                        imgByte = System.IO.File.ReadAllBytes(imPathBase + row.Id);
                    }
                    //viewRows.Add(new UpdateDictionaryRowCommand
                    //{
                    //    Id = row.Id,
                    //    WordText = row.WordText,
                    //    LearnStatus = row.LearnStatus,
                    //    LearnStatusChangedAt = row.LearnStatusChangedAt,
                    //    WordTranslation = row.WordTranslation,
                    //    WordContexts = row.WordContexts,
                    //    ImgBase64 = imgByte ==null? "": Convert.ToBase64String(imgByte),
                    //    NoteText = row.NoteText
                    //});
                }
                return StatusCode(200, UpdateDictionaryAndRowsCommand.Create(dictionaryResult.Value()));
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
        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateDictionaryAndRowsCommand model) {
            model.UpdateDictionaryCommand.LastViewedAt = DateTime.Now.ToUniversalTime();

            model.UpdateDictionaryCommand.UserId = Guid.Parse(User.FindFirstValue("Id"));

            var dictionaryResult = await sender.Send(model);

            if (dictionaryResult.IsSuccess) {
                return StatusCode(200, "Словарь обновлен!");
            }

            return StatusCode(400,"Не удалось обновить словарь");
        }

        /// <summary>
        /// Move words to another dictionary
        /// </summary>  
        /// <param name="model">source and target dictionary with word's id array model</param>
        /// <returns>Returns a success or an error message</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPatch("MoveToAnotherDictionary")]
        public async Task<IActionResult> PatchMoveToAnotherDicitonary([FromBody]MoveToAnotherDicitonaryRowsCommand model) {
            var result = await sender.Send(model);

            if (result.IsSuccess) {
                return StatusCode(200, "Словарь успешно перенесены!");
            }

            return StatusCode(400, "Не удалось перенести слова");
        }

        /// <summary>
        /// Get free dictionaries for create a new card plan
        /// </summary>  
        /// <returns>Returns a list of dictionaries or an error message</returns>
        /// <response code="200">The request was successful. Returns a list of dictionaries</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("GetFreeDicitonariesForCardPlan")]
        public async Task<IActionResult> GetFreeDicitonariesForCardPlan() {
            var model = new GetFreeDicitonariesForCardPlanQuery(Guid.Parse(User.FindFirstValue("Id")));
            
            var result = await sender.Send(model);

            if (result.IsSuccess) {
                return StatusCode(200, result.Value());
            }

            return StatusCode(400, "Не удалось найти свободные словари");
        }

        /// <summary>
        /// Update learn status of dictionary rows
        /// </summary>  
        /// <param name="model">Update the learn status of dictionary rows model</param>
        /// <returns>Returns a success or error message</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPatch("UpdateLearnStatus")]
        public async Task<IActionResult> Patch([FromBody]UpdateDictionaryRowsLearnStatusCommand model) {
            var updateResult = await sender.Send(model);

            if (updateResult.IsSuccess) {
                return StatusCode(200, "Словарь обновлен!");
            }

            return StatusCode(400, "Не удалось обновить словарь");
        }

        /// <summary>
        /// Get dictionary with rows by learn status
        /// </summary>
        /// <param name="id">Dictionary id</param>
        /// <param name="learnStatus">Dictionary id</param>
        /// <returns>Returns a ditionary with rows by learn status</returns>
        /// <response code="200">The request was successful. Returns a dictionary with rows</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("{id}/{learnStatus}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromRoute] LearnStatus learnStatus) {
            var dictionaryResult = await sender.Send(new GetDictionaryWithRowsByLearnStatusQuery { DictionaryId = Guid.Parse(id),DictionaryRowLearnStatus = learnStatus });

            if (dictionaryResult.IsSuccess) {
                return StatusCode(200, UpdateDictionaryAndRowsCommand.Create(dictionaryResult.Value()));
            }

            return StatusCode(400, "Не удалось найти словарь");
        }

        /// <summary>
        /// Get dictionaryViewModel with dictionaryRowsViewModel
        /// </summary>
        /// <param name="id">Dictionary id</param>
        /// <returns>Returns a ditionary with rows</returns>
        /// <response code="200">The request was successful. Returns a dictionary with rows</response>
        /// <response code="400">The request was a failure. Returns an error message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("GetDictionaryViewModelWithRowsViewModel/{id}")]
        public async Task<IActionResult> GetDictionaryViewModelWithRowsViewModel([FromRoute] string id) {
            var dictionaryResult = await sender.Send(new GetDictionaryViewModelWithRowsQuery { DictionaryId = Guid.Parse(id)});

            if (dictionaryResult.IsSuccess) {
                return StatusCode(200, DictionaryViewModel.Create(dictionaryResult.Value()));
            }

            return StatusCode(400, "Не удалось найти словарь");
        }
    }
}
