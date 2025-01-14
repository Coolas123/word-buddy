using Application.Abstractions.Messaging;
using Application.HelpClasses;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.Commands.ChangeSettings
{
    public sealed class ChangeUserSettingsCommandHandler : ICommandHandler<ChangeUserSettingsCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChangeUserSettingsCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(ChangeUserSettingsCommand request, CancellationToken cancellationToken) {
            bool isClaimsChanged = false;

            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user != null) {
                isClaimsChanged = user.ChangeSettings(
                    request.UserName,
                    request.Email,
                    request.Country,
                    request.Password != null ? HashPassword.Generate(request.Password) : null
                    );
            }

            userRepository.Update(user);

            await unitOfWork.SaveChangesAsync();

            if (isClaimsChanged) {
                return Result.Success(isClaimsChanged);
            }

            return Result.Success(false);
        }
    }
}
