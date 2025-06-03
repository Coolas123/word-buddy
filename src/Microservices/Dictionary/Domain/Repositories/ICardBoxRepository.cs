using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICardBoxRepository : IBaseRepository<CardBox>
    {
        Task CreateRangeAsync(IEnumerable<CardBox> cardBoxes);
    }
}
