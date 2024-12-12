using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Dictionaries.Queries.GetDictionary
{
    public sealed class GetDictionaryQuery : IQuery<Dictionary>
    {
        public Guid DictionaryId { get; set; }
    }
}
