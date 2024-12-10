using Domain.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Controllers.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static void AddModelError(this ModelStateDictionary modelStateDictionary, params Error[] errors) {
            if (errors.Length == 0) return;

            foreach (var error in errors) {
                modelStateDictionary.AddModelError(error.Code, error.Message);
            }
        }
    }
}
