using Application.Abstractions.Messaging;

namespace Application.GeneratedTextHistory.Commands.SaveGeneratedTextHistory
{
    public sealed class SaveGeneratedTextHistoryCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string Text { get; set; } = null!;
    }
}
