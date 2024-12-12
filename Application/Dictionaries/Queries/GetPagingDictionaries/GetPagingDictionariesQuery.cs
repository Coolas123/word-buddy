using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.TagHelperModels;

namespace Application.Dictionaries.Queries.GetPagingDictionaries
{
    public sealed class GetPagingDictionariesQuery : IQuery<(PagingInfo,IEnumerable<Dictionary>)>
    {
        public PagingInfo PagingInfo { get; set; }
        public Guid UserId { get; set; }
    }
}
