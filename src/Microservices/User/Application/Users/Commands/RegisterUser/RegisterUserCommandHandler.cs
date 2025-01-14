using Application.Abstractions.Messaging;
using Application.HelpClasses;
using Domain.Entities;
using Domain.EntityServices;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.Commands.RegisterUser
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailUniqueCheck emailUniqueCheck;
        private readonly IUnitOfWork unitOfWork;
        private readonly JWTGenerator JWTGenerator;

        public RegisterUserCommandHandler(
            IUserRepository userRepository, 
            IEmailUniqueCheck emailUniqueCheck, 
            IUnitOfWork unitOfWork,
            JWTGenerator JWTGenerator) {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.emailUniqueCheck = emailUniqueCheck;
            this.JWTGenerator = JWTGenerator;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {
            var userResult = await User.CreateAsync(
                Guid.NewGuid(),
                request.UserName,
                request.Email,
                request.Country,
                HashPassword.Generate(request.Password),
                Domain.Enums.SystemRole.User,
                Domain.Enums.UserRole.WordLearner,
                emailUniqueCheck);
            
            if (userResult.IsSuccess) {
                await userRepository.CreateAsync(userResult.Value());

                var jwtToken = JWTGenerator.Create(userResult.Value());
                if (jwtToken == null) {
                    Result.Failure<string>(ApplicationError.JWTError.TokenNull);
                }

                await unitOfWork.SaveChangesAsync();

                return Result.Success(jwtToken);
            }

            return Result.Failure<string>(Error.None);
        }
    }
}
