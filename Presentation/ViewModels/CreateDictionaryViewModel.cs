using Application.Dictionaries.Commands.CreateDictionary;
using Application.Dictionaries.Commands.CreateDictionaryRow;

namespace Presentation.ViewModels
{
    public sealed class CreateDictionaryViewModel
    {
        public CreateDictionaryCommand CreateDictionaryCommand { get; set; } = new();
        public CreateDictionaryRowCommand CreateDictionaryRowCommand { get; set; } = new();
    }
}
