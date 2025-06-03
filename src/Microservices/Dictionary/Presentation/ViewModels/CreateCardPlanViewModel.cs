
namespace Presentation.ViewModels
{
    public sealed class CreateCardPlanViewModel
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public IEnumerable<string> DictionariesId { get; set; } = Enumerable.Empty<string>();
    }
}
