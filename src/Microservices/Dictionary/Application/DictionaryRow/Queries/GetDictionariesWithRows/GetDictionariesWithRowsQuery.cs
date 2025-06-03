using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.DictionaryRow.Queries.GetDictionariesWithRows
{
    public class GetDictionariesWithRowsQuery: IQuery<IEnumerable<Dictionary>>
    {
        public IEnumerable<Guid> DictioanariesId { get; set; } = null!;
    }
}
