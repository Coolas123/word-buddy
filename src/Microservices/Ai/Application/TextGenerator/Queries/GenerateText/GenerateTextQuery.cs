using Application.Abstractions.Messaging;

namespace Application.TextGenerator.Queries.GenerateText
{
    public sealed class GenerateTextQuery : IQuery<string>
    {
        public string Prompt { get; set; } = null!;
    }
}
