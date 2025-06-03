using Application.CardPlan.Commands.CreateCardPlanAndUpdateDictionaryCardPlan;
using Application.CardPlan.Commands.UpdateCardPlan;
using Application.CardPlan.Commands.UpdateCardPlanAndDicitonaries;
using Application.CardPlan.Queries.GetCardPlanDictionariesId;
using Application.CardPlan.Queries.GetCardPlans;
using Application.CardPlan.Queries.GetCardPlanWithDictionariesAndCardBoxes;
using Application.Dictionaries.Commands.UpdateDictionaryCardPlan;
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
    [Route("CardPlans")]
    [Produces("application/json")]
    public class CardPlanController : ControllerBase
    {
        private readonly ISender sender;
        public CardPlanController(ISender sender) {
            this.sender = sender;
        }

        /// <summary>
        /// Create a card plan
        /// </summary>
        /// <param name="model">New card plan model</param>
        /// <returns>Returns a success message or a list of errors</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns a list of errors</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCardPlanViewModel model) {
            var newCaprdPlan = CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand
                .Create(model.Title, model.Description, model.DictionariesId, Guid.Parse(User.FindFirstValue("Id")));
            var createCardPlanResult = await sender.Send(newCaprdPlan);

            if (createCardPlanResult.IsSuccess) {
                return StatusCode(200, "План успешно создан!");
            }
            else if (createCardPlanResult is IValidationResult) {
                var errors = (IValidationResult)createCardPlanResult;
                return StatusCode(400, errors.Errors);
            }
            else {
                return StatusCode(400, createCardPlanResult.Error);
            }
        }

        /// <summary>
        /// Get card plans
        /// </summary>
        /// <returns>Returns a list of card plans or a list of errors</returns>
        /// <response code="200">The request was successful. Returns a list of card plans</response>
        /// <response code="400">The request was a failure. Returns a list of errors</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("GetCardPlans")]
        public async Task<IActionResult> GetCardPlans() {
            var model = new GetCardPlansCommand(Guid.Parse(User.FindFirstValue("Id")));
            var getCardPlansResult = await sender.Send(model);

            if (getCardPlansResult.IsSuccess) {
                return StatusCode(200, getCardPlansResult.Value());
            }
            else if (getCardPlansResult is IValidationResult) {
                var errors = (IValidationResult)getCardPlansResult;
                return StatusCode(400, errors.Errors);
            }
            else {
                return StatusCode(400, getCardPlansResult.Error);
            }
        }

        /// <summary>
        /// Get card plan
        /// </summary>
        /// <returns>Returns a card plan or a failure message</returns>
        /// <response code="200">The request was successful. Returns a card plan</response>
        /// <response code="400">The request was a failure. Returns a failure message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("GetCardPlanWithDictionaries/{id}")]
        public async Task<IActionResult> GetCardPlanWithDictionaries(string id) {
            var model = new GetCardPlansCommand(Guid.Parse(User.FindFirstValue("Id")));
            var getCardPlansResult = await sender.Send(model);

            if (getCardPlansResult.IsSuccess) {
                return StatusCode(200, getCardPlansResult.Value());
            }

            var validationError = (IValidationResult)getCardPlansResult;

            return StatusCode(400, validationError);
        }

        /// <summary>
        /// Get card plan with dictionaries and card boxes
        /// </summary>
        /// <returns>Returns a card plan with dictionaries and card boxes or a failure message</returns>
        /// <response code="200">The request was successful. Returns a card plan</response>
        /// <response code="400">The request was a failure. Returns a failure message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("GetCardPlanWithDictionariesAndCardBoxes")]
        public async Task<IActionResult> GetCardPlanWithDictionariesAndCardBoxes() {
            var model = new GetCardPlanWithDictionariesAndCardBoxesQuery(Guid.Parse(User.FindFirstValue("Id")));
            var getCardPlanResult = await sender.Send(model);

            if (getCardPlanResult.IsSuccess) {
                return StatusCode(200, getCardPlanResult.Value());
            }
            else if (getCardPlanResult is IValidationResult) {
                var errors = (IValidationResult)getCardPlanResult;
                return StatusCode(400, errors.Errors);
            }
            else {
                return StatusCode(400, getCardPlanResult.Error);
            }
        }

        /// <summary>
        /// Get card plan's dictionaries id
        /// </summary>
        /// <returns>Returns a list if dictionaries id</returns>
        /// <response code="200">The request was successful. Returns a list of dictionaries id</response>
        /// <response code="400">The request was a failure. Returns a failure message</response>
        [Authorize(Roles = "WordLearner")]
        [HttpGet("{cardPlanId}/{cardBoxStatus}")]
        public async Task<IActionResult> GetDictionariesId([FromRoute] string cardPlanId, [FromRoute] string cardBoxStatus) {
            var model = new GetCardPlanDictionariesIdByLearnStatusQuery(Guid.Parse(cardPlanId), (LearnStatus)Enum.Parse(typeof(LearnStatus),cardBoxStatus));
            var getDictionariesIdResult = await sender.Send(model);

            if (getDictionariesIdResult.IsSuccess) {
                var result = new CardPlanByCardBoxViewModel(
                    getDictionariesIdResult.Value().Id,
                    getDictionariesIdResult.Value().Dictionaries.Select(x => x.Id));
                
                return StatusCode(200, result);
            }
            else if (getDictionariesIdResult is IValidationResult) {
                var errors = (IValidationResult)getDictionariesIdResult;
                return StatusCode(400, errors.Errors);
            }
            else {
                return StatusCode(400, getDictionariesIdResult.Error);
            }
        }

        /// <summary>
        /// Update card plan
        /// </summary>
        /// <returns>Returns a success message or a list of errors</returns>
        /// <response code="200">The request was successful. Returns a success message</response>
        /// <response code="400">The request was a failure. Returns a list of errors</response>
        [Authorize(Roles = "WordLearner")]
        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateCardPlanViewModel model) {
            var command = new UpdateCardPlanAndDicitonariesCommand
            {
                UpdateCardPlanCommand = new UpdateCardPlanCommand(Guid.Parse(model.Id), Guid.Parse(User.FindFirstValue("Id")), model.Title, model.Description),
                UpdateNewDictionariesCardPlanCommand = new UpdateDictionariesCardPlanCommand(Guid.Parse(model.Id), model.NewDictionariesId),
                UpdateDeleteDictionariesCardPlanCommand = new UpdateDictionariesCardPlanCommand(null, model.DeleteDictionariesId)
            };
            var updateResult = await sender.Send(command);

            if (updateResult.IsSuccess) {

                return StatusCode(200);
            }
            else if (updateResult is IValidationResult) {
                var errors = (IValidationResult)updateResult;
                return StatusCode(400, errors.Errors);
            }
            else {
                return StatusCode(400, updateResult.Error);
            }
        }
    }
}
