using Application.HelpClasses;
using Application.Users.Commands.RegisterUser;
using AutoFixture;
using Domain.EntityServices;
using Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Api.UnitTests.User
{
    public class CommandHandlerTests
    {
        private readonly Mock<IUserRepository> userRepository;
        private readonly Mock<IEmailUniqueCheck> emailUniqueCheck;
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<JWTGenerator> jwtGenerator;
        public CommandHandlerTests() {
            userRepository = new Mock<IUserRepository>();
            emailUniqueCheck = new Mock<IEmailUniqueCheck>();
            unitOfWork = new Mock<IUnitOfWork>();
            jwtGenerator = new Mock<JWTGenerator>();
        }

        [Fact]
        public async Task ChangeUserSettingCommandValidation_UserName_ReturnsErrorWhenLengthTooLong() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x=>x.UserName, Enumerable.Repeat("n",65).ToString())
                .Create();

            var handler = 
                new RegisterUserCommandHandler(
                    userRepository.Object,
                    unitOfWork.Object,
                    jwtGenerator.Object
                );


            //Act
            var result = await handler.Handle(registerUserCommand,default);
            //Assert
            Assert.False(result.IsFailure);
        }
    }
}
