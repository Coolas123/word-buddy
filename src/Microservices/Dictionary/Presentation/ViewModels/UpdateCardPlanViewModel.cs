
namespace Presentation.ViewModels
{
    public sealed class UpdateCardPlanViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public IEnumerable<string>? NewDictionariesId { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string>? DeleteDictionariesId { get; set; } = Enumerable.Empty<string>();
    }
}
