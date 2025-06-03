//using Application.DictionaryRow.Commands.UpdateWord.UpdateWords;
//using Domain.Shared;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Presentation.Controllers
//{
//    [ApiController]
//    [Produces("application/json")]
//    public class DictionaryRowsController : ControllerBase
//    {
//        private readonly ISender sender;

//        public DictionaryRowsController(ISender sender) {
//            this.sender = sender;
//        }
//        [Authorize(Roles = "WordLearner")]
//        [HttpPatch]
//        public async Task<IActionResult> PatchDictionaryRows(UpdateDictionaryRowsCommand dictionaryRows) {
//            var result = await sender.Send(dictionaryRows);

//            if (result.IsFailure) {
//                var validationError = (IValidationResult)result;
//                return StatusCode(400, validationError);
//            }

//            return StatusCode(200,"Настройки успешно изменены");
//        }
//    }
//}
