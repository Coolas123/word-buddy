using Application.Abstractions.Messaging;

namespace Application.GeneratedTextHistory.Queries
{
    public sealed class GetGeneratedTextHistoryQuery : IQuery<IEnumerable<Domain.Entities.GeneratedTextHistory>>
    {
        public Guid UserId { get; set; }
    }
}
