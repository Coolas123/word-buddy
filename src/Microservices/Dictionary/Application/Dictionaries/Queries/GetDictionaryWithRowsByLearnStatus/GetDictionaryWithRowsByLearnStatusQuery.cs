using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;

namespace Application.DictionaryRow.Queries.GetDictionaryWithRowsByLearnStatus
{
    public sealed class GetDictionaryWithRowsByLearnStatusQuery : IQuery<Dictionary>
    {
        public Guid DictionaryId {  get; set; }
        public LearnStatus DictionaryRowLearnStatus { get; set; }
    }
}
