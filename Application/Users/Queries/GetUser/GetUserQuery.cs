using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Users.Queries.GetUser
{
    public sealed class GetUserQuery : IQuery<User>
    {
        public Guid UserId { get; init; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
