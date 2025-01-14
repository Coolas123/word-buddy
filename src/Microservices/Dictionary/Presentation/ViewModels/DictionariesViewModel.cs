using Domain.Entities;
using Domain.TagHelperModels;

namespace Presentation.ViewModels
{
    public sealed class DictionariesViewModel
    {
        public IEnumerable<Dictionary> Dictionaries { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
