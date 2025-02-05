using Application.HelpClasses;
using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.LoginUser;
using AutoFixture;
using Domain.Enums;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace User.Api.UnitTests.User
{
    public class CommandValidator
    {
        private readonly Mock<IUserRepository> userRepository;

        public CommandValidator() {
            userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task RegisterUserCommandValidator_UserName_ReturnsErrorWhenLengthTooLong() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.UserName, "n".PadRight(65,'n'))
                .Create();

            var expected = "прозвище должно быть не длинее 64 символов";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.UserName));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_UserNameReturnsErrorWhenEmpty() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.UserName, string.Empty)
                .Create();

            var expected = "Введите прозвище";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.UserName));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_UserNameReturnsErrorWhenIncorrectSymbols() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.UserName, "не латинский алфавит")
                .Create();

            var expected = "Прозвище должно состоять из латинских букв, цифр";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.UserName));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_PasswordReturnsErrorWhenEmpty() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Password, string.Empty)
                .Create();

            var expected = "Введите пароль";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Password));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_PasswordReturnsErrorWhenTooLong() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Password, "n".PadLeft(65))
                .Create();

            var expected = "Пароль не должен превышать 32 символа";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Password));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_PasswordReturnsErrorWhenTooShort() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Password, "n")
                .Create();

            var expected = "Пароль должен содержать минимум 6 символов";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Password));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_ConfirmPasswordReturnsErrorWhenEmpty() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.ConfirmPassword, string.Empty)
                .Create();

            var expected = "Повторите пароль";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.ConfirmPassword));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_ConfirmPasswordReturnsErrorWhenNotEqualToPassword() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.ConfirmPassword, "password")
                .With(x => x.Password, "password_WRONG")
                .Create();

            var expected = "Пароли не совпадают";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.ConfirmPassword));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_EmailReturnsErrorWhenEmpty() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Email, string.Empty)
                .Create();

            var expected = "Введите почту";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Email));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_EmailReturnsErrorWhenNotUnique() {
            //Arrange
            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                "dima@mail.ru",
                Country.Russia,
                HashPassword.Generate("aA1@123456"),
                SystemRole.User,
                UserRole.WordLearner
            );

            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Email, "dima@mail.ru")
                .Create();

            var expected = "Пользователь с такой почтой уже существует";

            userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userResult.Value()));

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Email));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_EmailReturnsErrorWhenIncorrectFormat() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Email, "incorrect_format")
                .Create();

            var expected = "Неверный формат почты";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Email));

            Assert.Equal(expected, error?.ErrorMessage);
        }

        [Fact]
        public async Task RegisterUserCommandValidator_EmailReturnsErrorWhenTooLong() {
            //Arrange
            var registerUserCommand = new Fixture()
                .Build<RegisterUserCommand>()
                .With(x => x.Email,"@mail.ru".PadLeft(65,'n'))
                .Create();

            var expected = "Почта не должна превышать 64 символа";

            var validator = new RegisterUserCommandValidator(userRepository.Object);

            //Act
            var result = await validator.TestValidateAsync(registerUserCommand);
            //Assert
            var error = result.Errors.SingleOrDefault(
                x => x.PropertyName == nameof(registerUserCommand.Email));

            Assert.Equal(expected, error?.ErrorMessage);
        }
    }
}
