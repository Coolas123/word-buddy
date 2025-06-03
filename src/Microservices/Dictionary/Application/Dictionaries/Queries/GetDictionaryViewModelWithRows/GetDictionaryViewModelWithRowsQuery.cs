using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Dictionaries.Queries.GetDictionaryViewModelWithRows
{
    public sealed class GetDictionaryViewModelWithRowsQuery : IQuery<Dictionary>
    {
        public Guid DictionaryId { get; set; }
    }
}
