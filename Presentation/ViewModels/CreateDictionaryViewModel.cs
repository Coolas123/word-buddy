using Application.Dictionaries.Commands.CreateDictionary;

namespace Presentation.ViewModels
{
    public sealed class CreateDictionaryViewModel
    {
        public CreateDictionaryCommand? CreateDictionaryCommand { get; set; }
        public List<DictionaryRowViewModel> DictionaryRowViewModels { get; set; } = new();
    }
}
