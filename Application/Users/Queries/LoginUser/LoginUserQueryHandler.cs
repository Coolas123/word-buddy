using Application.Abstractions.Messaging;
using Application.HelpClasses;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.Queries.LoginUser
{
    public sealed class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, string>
    {
        private readonly IUserRepository userRepository;
        private readonly JWTGenerator JWTGenerator;
        public LoginUserQueryHandler(IUserRepository userRepository, JWTGenerator jWTGenerator) {
            this.userRepository = userRepository;
            JWTGenerator = jWTGenerator;
        }
        public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);

            var token = JWTGenerator.Create(user);

            if (token != null) {
                return Result.Success(JWTGenerator.Create(user));
            }
            return Result.Failure<string>(ApplicationError.JWTError.TokenNull);
        }
    }
}
