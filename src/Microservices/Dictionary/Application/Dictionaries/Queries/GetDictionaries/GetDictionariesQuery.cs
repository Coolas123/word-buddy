using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Dictionaries.Queries.GetDictionaries
{
    public sealed class GetDictionariesQuery : IQuery<IEnumerable<Dictionary>>
    {
        public Guid UserId { get; set; }
    }
}
