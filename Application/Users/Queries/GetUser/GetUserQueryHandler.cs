using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.Queries.GetUser
{
    internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
    {
        private readonly IUserRepository userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user != null)
                return Result.Success(user);

            return Result.Failure<User>(ApplicationError.User.UserNotFound);
        }
    }
}
