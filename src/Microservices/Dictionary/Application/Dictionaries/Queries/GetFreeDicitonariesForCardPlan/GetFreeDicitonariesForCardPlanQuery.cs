using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Dictionaries.Queries.GetFreeDicitonariesForCardPlan
{
    public class GetFreeDicitonariesForCardPlanQuery:IQuery<IEnumerable<Dictionary>>
    {
        public Guid UserId { get; set; }

        public GetFreeDicitonariesForCardPlanQuery(Guid userId) {
            UserId = userId;
        }
    }
}
