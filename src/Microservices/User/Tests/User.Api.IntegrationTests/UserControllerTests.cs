using Application.HelpClasses;
using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.LoginUser;
using Domain.EntityServices;
using Domain.Enums;
using Domain.Shared;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace User.Api.IntegrationTests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task RegisterUser_ReturnsStatusCode201_WhenSuccessfulyCreated() {
            //Arrange
            var registerUserCommand = new RegisterUserCommand
            {
                UserName = "dima",
                Country = Country.Russia,
                Password = "aA1@123456",
                ConfirmPassword = "aA1@123456",
                Email = "dima@mail.ru"
            };

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();

                //Act
                var response = await client.PostAsJsonAsync("https://localhost:5011/Users/Register", registerUserCommand);

                //Assert
                Assert.NotNull(response);

                var resultMessage = await response.Content.ReadAsStringAsync();
                Assert.True(!string.IsNullOrWhiteSpace(resultMessage));

                Assert.True(response.StatusCode.Equals(HttpStatusCode.Created));
            }
        }
        [Fact]
        public async Task RegisterUser_ReturnsErrorsAndStatusCode400_WhenUnsuccessfulyCreated() {
            //Arrange
            var registerUserCommand = new RegisterUserCommand
                {
                    UserName = "dima",
                    Country = Country.Russia,
                    Password = "aA1@123456",
                    ConfirmPassword = "aA1123456",
                    Email = "dima@mail.ru"
                };

            Mock<IEmailUniqueCheck> mock = new Mock<IEmailUniqueCheck>();
            mock.Setup(x => x.IsUnique(It.IsAny<string>())).ReturnsAsync(() => false);
            var r = mock.Object;
            using (var application = new UserWebApplicationFactory()) {
                application.ChangeDependency(mock.Object);

                var client = application.CreateClient();
                //Act
                var response = await client.PostAsJsonAsync("https://localhost:5011/Users/Register", registerUserCommand);
                //Assert
                Assert.NotNull(response);

                var resultMessage = await response.Content.ReadFromJsonAsync<IEnumerable<Error>>();
                Assert.NotNull(resultMessage);
                Assert.NotEmpty(resultMessage);

                Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest));
            }
        }

        [Fact]
        public async Task PostLogin_ReturnsStatusCode200_WhenSuccessfulyAuthenticated() {
            //Arrange
            var loginUserQuery = new LoginUserQuery
            {
                Password = "aA1@123456",
                Email = "dima@mail.ru"
            };

            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                loginUserQuery.Email,
                Country.Russia,
                HashPassword.Generate(loginUserQuery.Password),
                SystemRole.User,
                UserRole.WordLearner
            );

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();
                await application.SetDataAsync(userResult.Value());
                //Act
                var responseLogin = await client.PostAsJsonAsync("https://localhost:5011/Users/Login", loginUserQuery);
                //Assert
                Assert.NotNull(responseLogin);

                var resultMessage = await responseLogin.Content.ReadAsStringAsync();
                Assert.True(!string.IsNullOrWhiteSpace(resultMessage));

                Assert.True(responseLogin.StatusCode.Equals(HttpStatusCode.OK));
            }
        }

        [Fact]
        public async Task PostLogin_ReturnsStatusCode400_WhenUnsuccessfulyAuthenticated() {
            //Arrange
            var loginUserQuery = new LoginUserQuery
            {
                Password = "aA1@123456_WRONG",
                Email = "dima@mail.ru"
            };
            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                loginUserQuery.Email,
                Country.Russia,
                HashPassword.Generate("aA1@123456"),
                SystemRole.User,
                UserRole.WordLearner
            );

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();
                await application.SetDataAsync(userResult.Value());
                //Act
                var responseLogin = await client.PostAsJsonAsync("https://localhost:5011/Users/Login", loginUserQuery);
                //Assert
                Assert.NotNull(responseLogin);

                var resultMessage = await responseLogin.Content.ReadFromJsonAsync<IEnumerable<Error>>();
                Assert.NotNull(resultMessage);
                Assert.NotEmpty(resultMessage);

                Assert.True(responseLogin.StatusCode.Equals(HttpStatusCode.BadRequest));
            }
        }

        [Fact]
        public async Task LogOut_ReturnsStatusCode200AndDeleteAuthenticationHeader_WhenSuccessfulyUnauthenticated() {
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

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();
                var jwtGenerator = application.GetDependency<JWTGenerator>();
                var jwtToken = jwtGenerator.Create(userResult.Value());
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtToken}");
                //Act
                var responseLogOut = await client.GetAsync("https://localhost:5011/Users/LogOut");
                //Assert
                Assert.NotNull(responseLogOut);

                Assert.False(responseLogOut.Headers.Contains("Authorization"));

                Assert.True(responseLogOut.StatusCode.Equals(HttpStatusCode.OK));
            }
        }
    }
}
